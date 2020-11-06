using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.DTO;
using Server.Models.Entities;
using Server.Models.VO;
using Server.Services;
using File = Server.Models.Entities.File;

namespace Server.Controllers
{
    [Route("api/storage/file")]
    [ApiController]
    [NeedPermission(PermissionBank.StorageFileUploadBasic)]
    public class FileUploadController : Controller
    {
        private IDatabaseService _databaseService;
        
        private ITencentCos _tencentCos;

        private readonly ILogger _logger;


        public FileUploadController(IDatabaseService databaseService, ITencentCos tencentCos, ILogger<GlobalExceptionFilter> logger)
        {
            _databaseService = databaseService;
            _tencentCos = tencentCos;
            _logger = logger;
        }

        
        [HttpPost]
        public IActionResult RequestUpload([FromBody] FileUploadRequestModel requestModel)
        {
            if (!(HttpContext.Items["actor"] is User loginUser))
            {
                throw new UnexpectedException();
            }


            // 根据上传路径判断权限
            // /users/用户名/XXX => .HasPermission('file.upload.user.用户名')
            // /groups/组名/XXX => .HasPermission('file.upload.group.组名')


            var ofile = _databaseService.Files.
                FirstOrDefault(s => 
                        s.Md5 == requestModel.Md5.ToLower() &&                  // 哈希相等
                        s.Size == requestModel.Size &&                          // 文件大小相等
                        s.Status == Models.Entities.File.FileStatus.Confirmed   // 文件已上传完
                );


            var file = new File();
            if (ofile != null)
            {
                file.Path = requestModel.Path;
                file.Type = requestModel.Type;
                file.Folder = Path.GetDirectoryName(requestModel.Path) ?? "";
                file.Name = Path.GetFileName(requestModel.Path) ?? "";

                file.Guid = ofile.Guid;
                file.StorageName = ofile.StorageName;
                file.User = loginUser;
                file.Status = Models.Entities.File.FileStatus.Pending;
                file.Size = ofile.Size;
                file.Md5 = ofile.Md5.ToLower();
            }
            else
            {
                file.Path = requestModel.Path;
                file.Type = requestModel.Type;
                file.Folder = Path.GetDirectoryName(requestModel.Path) ?? "";
                file.Name = Path.GetFileName(requestModel.Path) ?? "";

                file.Guid = Guid.NewGuid().ToString();
                // TODO 检查 Guid 是否有重复
                file.StorageName = $"{file.Guid[0]}{file.Guid[1]}/{file.Guid[2]}{file.Guid[3]}/{file.Guid}{Path.GetExtension(requestModel.Path)}";
                file.User = loginUser;
                file.Status = Models.Entities.File.FileStatus.Pending;
                file.Size = requestModel.Size;
                file.Md5 = requestModel.Md5.ToLower();
            }
            
            _databaseService.Files.Add(file);

            Dictionary<string, object> token;
            try
            {
                token = _tencentCos.GetToken(file);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, e.Data);
                throw;
            }

            _databaseService.SaveChanges();

            return Ok(new FileUploadRequestResultModel(file, token));
        }


    }
}
