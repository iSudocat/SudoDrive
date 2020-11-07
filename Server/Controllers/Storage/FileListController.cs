using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.DTO;
using Server.Models.Entities;
using Server.Models.VO;
using Server.Services;
using EntityFile = Server.Models.Entities.File;

namespace Server.Controllers.Storage
{
    [Route("api/storage/file")]
    [ApiController]
    [NeedPermission(PermissionBank.StorageFileListBasic)]
    public class FileListController : AbstractController
    {
        private IDatabaseService _databaseService;
        
        private readonly ILogger _logger;
        
        public FileListController(IDatabaseService databaseService, ILogger<GlobalExceptionFilter> logger)
        {
            _databaseService = databaseService;
            _logger = logger;
        }

        
        [HttpGet]
        public IActionResult ListFile([FromQuery] FileListRequestModel requestModel)
        {
            if (!(HttpContext.Items["actor"] is User loginUser))
            {
                throw new UnexpectedException();
            }

            requestModel.Folder = requestModel.Folder.Replace("/", "\\");
            var result = _databaseService.Files.Where(s => s.Folder == requestModel.Folder);

            // 添加其他的搜索条件


            result = result.Skip(requestModel.Offset);
            result = result.Take(requestModel.Amount);

            return Ok(new FileListResultModel(result, requestModel.Amount, requestModel.Offset));
        }
    }
}
