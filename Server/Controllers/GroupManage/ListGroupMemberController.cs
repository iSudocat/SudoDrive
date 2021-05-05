using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.Entities;
using Server.Services;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Server.Models.DTO;
using Server.Models.VO;
using EntityFile = Server.Models.Entities.File;

namespace Server.Controllers.GroupManage
{
    [Route("api/group/{groupname}/member")]
    [ApiController]
    public class ListGroupMemberController : AbstractController
    {
        private IDatabaseService _databaseService;
        public ListGroupMemberController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public IActionResult ListGroupMember([FromQuery] GroupMemberListRequestModel requestModel, string groupname)
        {
            if (!Regex.IsMatch(groupname, @"^[a-zA-Z0-9-_]{4,16}$"))
            {
                throw new GroupnameInvalidException("The groupname you enter is invalid when trying to add a member to it.");
            }
            var group = _databaseService.Groups.FirstOrDefault(t => t.GroupName == groupname);
            if (group == null)
            {
                throw new GroupNotExistException("The groupname you enter does not exsit actually when trying to add a grouptouser.");
            }
            
            string permission = PermissionBank.GroupOperationPermission(group.GroupName, "member", "list");
            if (!(HttpContext.Items["actor"] is User loginUser))
            {
                throw new UnexpectedException();
            }
            if (loginUser.HasPermission(permission) != true)
            {
                throw new AuthenticateFailedException("not has enough permission when trying to list members to a group.");
            }

            // 开始查找
            var result = _databaseService.GroupsToUsersRelation
                .Include(s => s.User).AsSingleQuery()
                .Where(s => s.GroupId == group.Id);
            
            
            // 按用户名关键字匹配
            if (requestModel.Username?.Length > 0)
            {
                foreach (var c in requestModel.Username)
                {
                    result = result.Where(s => s.User.Nickname.Contains(c));
                }
            }
            
            result = result.Skip(requestModel.Offset);
            result = result.Take(requestModel.Amount);
            
            return Ok(new GroupMemberListResultModel(group, result.ToList(), requestModel.Amount, requestModel.Offset));
        }
    }
}