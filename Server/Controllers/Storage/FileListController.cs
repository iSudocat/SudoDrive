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
        public IActionResult ListFile([FromQuery] FileListRequestModel requestRequestModel)
        {
            if (!(HttpContext.Items["actor"] is User loginUser))
            {
                throw new UnexpectedException();
            }

            var result = _databaseService.Files.Where(s => s.Folder == requestRequestModel.Folder);

            // 添加其他的搜索条件


            result = result.Skip(requestRequestModel.Offset);
            result = result.Take(requestRequestModel.Amount);

            return Ok(new FileListResultModel(result, requestRequestModel.Amount, requestRequestModel.Offset));
        }
    }
}
