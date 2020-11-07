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

            // 检查被确认文件是否存在
            var file = _databaseService.Files.FirstOrDefault(s => 
                s.Id == requestRequestModel.Id && 
                s.Guid == requestRequestModel.Guid &&
                s.Status == Models.Entities.File.FileStatus.Pending &&
                s.User == loginUser);

            if (file == null)
            {
                throw new ConfirmingFileNotFoundException();
            }

            // 检查被确认的文件是否正常
            var efile = _databaseService.Files.FirstOrDefault(s =>
                s.Id != file.Id &&                          // 不是同一个文件
                s.Path == file.Path &&                      // 但是是同一个路径
                s.Status == EntityFile.FileStatus.Confirmed // 并且是已经确认上传的内容
            );
            if (efile != null)
            {
                throw new ConfirmingFileNotFoundException();
            }
            
            // 确认上传
            file.Status = Models.Entities.File.FileStatus.Confirmed;


            // 建立文件夹
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

            // TODO 确认保存后删除无用记录

            _databaseService.SaveChanges();
            
            return Ok(file.ToVo());
        }
    }
}
