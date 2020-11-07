using System;
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

            var result = _databaseService.Files.AsQueryable();

            // 按文件夹查找
            if (!string.IsNullOrEmpty(requestModel.Folder))
            {
                requestModel.Folder = requestModel.Folder.Replace("/", "\\");
                result = result.Where(s => s.Folder == requestModel.Folder);
            }

            // 按路径前缀查找
            if (!string.IsNullOrEmpty(requestModel.PathPrefix))
            {
                requestModel.PathPrefix = requestModel.PathPrefix.Replace("/", "\\");
                result = result.Where(s => s.Folder.StartsWith(requestModel.PathPrefix));
            }

            // 按路径包含内容查找
            if (requestModel.PathContains?.Length > 0)
            {
                result = requestModel.PathContains.Aggregate(result,
                    (current, t) => current.Where(s => s.Path.Contains(t)));
            }

            // 按文件名包含内容查找
            if (requestModel.NameContains?.Length > 0)
            {
                result = requestModel.NameContains.Aggregate(result,
                    (current, t) => current.Where(s => s.Path.Contains(t)));
            }

            // 按照文件类型查找
            if (requestModel.Type?.Length > 0)
            {
                result = result.Where(s => requestModel.Type.Contains(s.Type));
            }

            // TODO 按照用户权限添加筛选

            // 添加其他的搜索条件

            result = result.Skip(requestModel.Offset);
            result = result.Take(requestModel.Amount);

            return Ok(new FileListResultModel(result, requestModel.Amount, requestModel.Offset));
        }
    }
}
