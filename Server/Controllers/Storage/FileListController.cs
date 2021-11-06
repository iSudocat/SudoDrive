using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

        private ITencentCos _tencentCos;

        private readonly ILogger _logger;

        private readonly TencentCosManagementModel _tencentCosManagement;

        public FileListController(IDatabaseService databaseService, ITencentCos tencentCos, IOptions<TencentCosManagementModel> tencentCosManagement, ILogger<GlobalExceptionFilter> logger)
        {
            _databaseService = databaseService;
            _tencentCos = tencentCos;
            _tencentCosManagement = tencentCosManagement.Value;
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

            // 过滤掉未确认的文件
            result = result.Where(s => s.Status == EntityFile.FileStatus.Confirmed);

            // 注入用户权限
            if (loginUser.HasPermission(PermissionBank.StoragePermission("root", "root", "list")) != true)
            {
                List<string> filter = new List<string>();
                filter.Add("everyone");

                if (loginUser.HasPermission(PermissionBank.StoragePermission("users", loginUser.Username, "list")) != false)
                {
                    filter.Add($"users.{loginUser.Username}");
                }
                foreach (var groupToUser in loginUser.GroupToUser)
                {
                    var groupName = groupToUser.Group.GroupName;
                    if (loginUser.HasPermission(PermissionBank.StoragePermission("groups", groupName, "list")) != false)
                    {
                        filter.Add($"groups.{groupName}");
                    }
                }

                var groups = loginUser.GroupToUser;
                foreach (var groupToUser in groups)
                {
                    var group = groupToUser.Group;
                    var permissions = group.GroupToPermission;
                    foreach (var groupToPermission in permissions)
                    {
                        var permission = groupToPermission.Permission;

                        var permissionNode = permission.Split(".");

                        // storage.file.operation.{type}.{name}.{operation}

                        if ((permissionNode.Length == 6) && (permissionNode[0] == "storage") && (permissionNode[1] == "file") && (permissionNode[2] == "operation"))
                        {
                            var type = permissionNode[3];
                            var name = permissionNode[4];
                            var operation = permissionNode[5];
                            if (operation != "list") continue;

                            switch (type)
                            {
                                case "users":
                                    filter.Add($"users.{name}");
                                    break;
                                case "groups":
                                    filter.Add($"groups.{name}");
                                    break;
                            }
                        }
                    }
                }

                result = result.Where(s => filter.Contains(s.Permission));
            }

            // 按文件夹查找
            if (!string.IsNullOrEmpty(requestModel.Folder))
            {
                requestModel.Folder = requestModel.Folder.Replace("\\", "/");
                result = result.Where(s => s.Folder == requestModel.Folder);
            }

            // 按路径前缀查找
            if (!string.IsNullOrEmpty(requestModel.PathPrefix))
            {
                requestModel.PathPrefix = requestModel.PathPrefix.Replace("\\", "/");
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
                    (current, t) => current.Where(s => s.Name.Contains(t)));
            }

            // 按照文件类型查找
            if (requestModel.Type?.Length > 0)
            {
                result = result.Where(s => requestModel.Type.Contains(s.Type));
            }

            // 按照路径全字匹配
            if (requestModel.Path?.Length > 0)
            {
                result = result.Where(s => requestModel.Path.Contains(s.Path));
            }
            
            // 按照 ID 匹配
            if (requestModel.Id?.Length > 0)
            {
                result = result.Where(s => requestModel.Id.Contains(s.Id));
            }

            // 按照 Guid 匹配
            if (requestModel.Guid?.Length > 0)
            {
                result = result.Where(s => requestModel.Guid.Contains(s.Guid));
            }

            // 添加其他的搜索条件

            result = result.Skip(requestModel.Offset);
            result = result.Take(requestModel.Amount);

            Dictionary<string, object> token = null;

            if (requestModel.Download == true)
            {
                List<string> resourcesList = new List<string>();
                foreach (var file in result)
                {
                    if (file.Type != "text/directory")
                    {
                        resourcesList.Add(file.StorageName);
                    }
                }

                try
                {
                    token = _tencentCos.GetDownloadToken(resourcesList);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message, e.Data);
                    throw new UnexpectedException(e.Message);
                }
            }
            
            return Ok(new FileListResultModel(result, requestModel.Amount, requestModel.Offset, token, _tencentCosManagement));
        }
    }
}
