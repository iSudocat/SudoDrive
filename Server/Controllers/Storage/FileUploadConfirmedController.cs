using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.Entities;
using Server.Models.VO;
using Server.Services;
using EntityFile = Server.Models.Entities.File;

namespace Server.Controllers.Storage
{
    [Route("api/storage/file")]
    [ApiController]
    [NeedPermission(PermissionBank.StorageFileUploadBasic)]
    public class FileUploadConfirmedController : AbstractController
    {
        private IDatabaseService _databaseService;
        

        private readonly ILogger _logger;


        public FileUploadConfirmedController(IDatabaseService databaseService, ILogger<GlobalExceptionFilter> logger)
        {
            _databaseService = databaseService;
            _logger = logger;
        }

        
        [HttpPatch]
        public IActionResult ConfirmUpload([FromBody] FileUploadConfirmRequestModel requestRequestModel)
        {
            if (!(HttpContext.Items["actor"] is User loginUser))
            {
                throw new UnexpectedException();
            }

            var file = _databaseService.Files.FirstOrDefault(s => 
                s.Id == requestRequestModel.Id && 
                s.Guid == requestRequestModel.Guid &&
                s.Status == Models.Entities.File.FileStatus.Pending &&
                s.User == loginUser);

            if (file == null)
            {
                throw new ConfirmingFileNotFoundException();
            }

            file.Status = Models.Entities.File.FileStatus.Confirmed;
            // 确认上传

            string folder = file.Folder;
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
                var s = folderPath[i];

                // a/b/c
                var n = folderPath[i - 1];
                

                // 有待商榷
                var t = _databaseService.Files.FirstOrDefault(t => t.Folder == s && t.Path == n);

                if (t == null)
                {
                    var tmp = new EntityFile
                    {
                        // Name: 文件夹名
                        Name = Path.GetFileName(n),
                        // Type: "text/directory"
                        Type = "text/directory",
                        // Folder: 上层目录名
                        Folder = s,
                        // Path: 该文件夹的路径: Folder + Name
                        Path = n,
                        // Guid: [空]
                        Guid = null,
                        // StorageName: [空]
                        StorageName = "",
                        // User: 创建的用户
                        User = loginUser,
                        // Status: 已确认
                        Status = EntityFile.FileStatus.Confirmed,
                        // Size: [空]
                        Size = 0,
                        // Md5: [空]
                        Md5 = null
                    };

                    _databaseService.Files.Add(tmp);
                }
            }


            _databaseService.SaveChanges();
            
            return Ok(file.ToVo());
        }
    }
}
