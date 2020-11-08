using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Controllers;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.DTO;
using Server.Models.Entities;
using Server.Models.VO;
using Server.Services;
using EntityFile = Server.Models.Entities.File;

namespace Server.Exceptions
{
    [Route("api/storage/file")]
    [ApiController]
    [NeedPermission(PermissionBank.StorageFileUploadBasic)]
    public class FileUploadRequestController : AbstractController
    {
        private IDatabaseService _databaseService;
        
        private ITencentCos _tencentCos;

        private readonly ILogger _logger;


        public FileUploadRequestController(IDatabaseService databaseService, ITencentCos tencentCos, ILogger<GlobalExceptionFilter> logger)
        {
            _databaseService = databaseService;
            _tencentCos = tencentCos;
            _logger = logger;
        }

        public void RequirePermission(string path, string uploadType, User loginUser, string operation)
        {
            string type;
            string name;

            var splitsPath = path.Split("/");

            if ((uploadType == "text/directory" && splitsPath.Length >= 3) || (splitsPath.Length >= 4))
            {
                // 获取上传路径的第一季第二级目录名
                type = splitsPath[1];
                name = splitsPath[2];

                switch (type)
                {
                    case "users":
                    case "groups":
                        break;
                    default:
                        type = "root";
                        break;
                }
            }
            else
            {
                // 如果是非 root 的状态
                type = "root";
                name = "";
            }

            // 检查用户权限
            var ret = loginUser.HasPermission(PermissionBank.StoragePermission(type, name, operation));
            if (ret == null)
            {
                // 检查用户默认
                if (type == "users" && name == loginUser.Username)
                {
                    ret = true;
                }
                else if (type == "groups")
                {
                    foreach (var groupToUser in loginUser.GroupToUser)
                    {
                        if (groupToUser.Group.GroupName == name) ret = true;
                    }
                }
                else ret = false;
            }

            if (ret != true)
            {
                throw new AuthenticateFailedException();
            }
        }


        [HttpPost]
        public IActionResult RequestUpload([FromBody] FileUploadRequestModel requestModel)
        {
            if (!(HttpContext.Items["actor"] is User loginUser))
            {
                throw new UnexpectedException();
            }

            requestModel.Path = requestModel.Path.Replace("\\", "/");
            if (requestModel.Path[0] != '/') requestModel.Path = "/" + requestModel.Path;

            this.RequirePermission(requestModel.Path, requestModel.Type, loginUser, "upload");

            if (requestModel.Type == "text/directory")
            {
                return RequestUploadFolder(requestModel, loginUser);
            }

            // 判断文件重复
            var efile = _databaseService.Files.FirstOrDefault(t =>
                t.Path == requestModel.Path &&
                t.Status == EntityFile.FileStatus.Confirmed
            );
            if (efile != null)
            {
                throw new FileNameDuplicatedException();
            }
            return RequestUploadFile(requestModel, loginUser);
        }

        private IActionResult RequestUploadFolder(FileUploadRequestModel requestModel, User loginUser)
        {
            // 建立文件夹
            string folder = requestModel.Path;
            List<string> folderPath = new List<string>();

            folderPath.Add(folder);
            while (folder != null && folder != "/" && folder != "\\")
            {
                var s = Path.GetDirectoryName(folder);
                folderPath.Add(folder = s);
            }

            for (int i = folderPath.Count - 1; i > 0; i--)
            {
                // a/b
                var s = folderPath[i].Replace("\\", "/");

                // a/b/c
                var n = folderPath[i - 1].Replace("\\", "/");

                var t = _databaseService.Files.FirstOrDefault(t =>
                    t.Path == n &&
                    t.Status == EntityFile.FileStatus.Confirmed
                );

                if (t == null)
                {
                    var tmp = EntityFile.CreateDirectoryRecord(Path.GetFileName(n), s, n, loginUser);
                    _databaseService.Files.Add(tmp);
                }
                else if (t.Type != "text/directory")
                {
                    throw new FileNameDuplicatedException();
                }
            }


            _databaseService.SaveChanges();

            var ret = _databaseService.Files.FirstOrDefault(t =>
                t.Type == "text/directory" &&
                t.Path == requestModel.Path
            );

            if (ret == null)
            {
                throw new UnexpectedException();
            }

            SetApiResultStatus(ApiResultStatus.StorageUploadSkip);
            return Ok(new FileUploadRequestResultModel(ret, null));
        }


        private IActionResult RequestUploadFile(FileUploadRequestModel requestModel, User loginUser)
        {
            var ofile = _databaseService.Files.
                FirstOrDefault(s =>
                        s.Md5 == requestModel.Md5.ToLower() &&                  // 哈希相等
                        s.Size == requestModel.Size &&                          // 文件大小相等
                        s.Status == Models.Entities.File.FileStatus.Confirmed   // 文件已上传完
                );

            var file = new EntityFile();
            if (ofile != null)
            {
                file.Path = requestModel.Path;
                file.Type = requestModel.Type;
                file.Folder = Path.GetDirectoryName(requestModel.Path) ?? "";
                file.Name = Path.GetFileName(requestModel.Path) ?? "";

                file.Guid = ofile.Guid;
                file.StorageName = ofile.StorageName;
                file.User = loginUser;
                file.Status = Models.Entities.File.FileStatus.Confirmed;
                file.Size = ofile.Size;
                file.Md5 = ofile.Md5.ToLower();
            }
            else
            {
                file.Path = requestModel.Path;
                file.Type = requestModel.Type;
                file.Folder = Path.GetDirectoryName(requestModel.Path) ?? "";
                file.Name = Path.GetFileName(requestModel.Path) ?? "";

                file.Guid = Guid.NewGuid().ToString().ToLower();
                // TODO 检查 Guid 是否有重复
                file.StorageName = $"{file.Guid[0]}{file.Guid[1]}/{file.Guid[2]}{file.Guid[3]}/{file.Guid}{Path.GetExtension(requestModel.Path)}";
                file.User = loginUser;
                file.Status = Models.Entities.File.FileStatus.Pending;
                file.Size = requestModel.Size;
                file.Md5 = requestModel.Md5.ToLower();
            }

            file.GetPermission();
            _databaseService.Files.Add(file);


            Dictionary<string, object> token;

            if (file.Status == Models.Entities.File.FileStatus.Pending)
            {
                try
                {
                    token = _tencentCos.GetToken(file);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message, e.Data);
                    throw new UnexpectedException();
                }

                SetApiResultStatus(ApiResultStatus.StorageUploadContinue);
            }
            else
            {
                token = null;

                SetApiResultStatus(ApiResultStatus.StorageUploadSkip);
            }

            _databaseService.SaveChanges();

            return Ok(new FileUploadRequestResultModel(file, token));
        }
    }
}
