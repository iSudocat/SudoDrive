using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.Entities;
using Server.Models.VO;
using Server.Services;
using System.Linq;
using Server.Models.DTO;
using EntityFile = Server.Models.Entities.File;

namespace Server.Controllers.GroupManage
{
    [Route("api/group")]
    [ApiController]
    [NeedPermission(PermissionBank.GroupManageGroupAdminBasic)]
    public class ListGroupController : AbstractController
    {
        private IDatabaseService _databaseService;
        public ListGroupController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public IActionResult ListGroup([FromQuery] GroupListRequestModel requestModel)
        {
            if (!(HttpContext.Items["actor"] is User loginUser))
            {
                throw new UnexpectedException();
            }

            var result = _databaseService.Groups.AsQueryable();
            
            // 按用户名关键字匹配
            if (requestModel.GroupName?.Length > 0)
            {
                foreach (var c in requestModel.GroupName)
                {
                    result = result.Where(s => s.GroupName.Contains(c));
                }
            }
            
            result = result.Skip(requestModel.Offset);
            result = result.Take(requestModel.Amount);
            
            return Ok(new GroupListResultModel(result.ToList(), requestModel.Amount, requestModel.Offset));
        }
    }
}