using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Middlewares;
using Server.Models.DTO;
using Server.Models.Entities;
using Server.Models.VO;
using Server.Services;
using File = Server.Models.Entities.File;

namespace Server.Controllers
{
    [Route("api/file/upload")]
    [ApiController]
    [NeedPermission("file.upload.basic")]
    public class UploadController : Controller
    {
        private IDatabaseService _databaseService;

        private readonly User _loginUser;

        public UploadController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;

            _loginUser = HttpContext.Items["actor"] as User;
            if (_loginUser == null)
            {
                throw new UnexpectedException();
            }
        }

        
        [HttpPost]
        public IActionResult RequestUpload([FromBody] UploadRequestModel requestModel)
        {
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
                file.User = _loginUser;
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
                file.StorageName = file.Guid[0] + file.Guid[1] + "/" + file.Guid[2] + file.Guid[3] + "/" + file.Guid;
                file.User = _loginUser;
                file.Status = Models.Entities.File.FileStatus.Pending;
                file.Size = requestModel.Size;
                file.Md5 = requestModel.Md5.ToLower();
            }

            _databaseService.Files.Add(file);

            return Ok();
        }


    }
}
