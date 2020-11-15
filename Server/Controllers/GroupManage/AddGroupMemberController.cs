using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.DTO;
using Server.Models.Entities;
using Server.Models.VO;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Server.Controllers.GroupManage
{
    [Route("api/group/{groupname}/member")]
    [ApiController]
    [NeedPermission(PermissionBank.GroupManageGroupMemberAddBasic)]
    public class AddGroupMemberController : AbstractController
    {
        private IDatabaseService _databaseService;
        public AddGroupMemberController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpPost]
        public IActionResult AddGroupMember([FromBody] GroupAddMemberRequestModel addGroupMemberRequestModel, string groupname)
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
            string permission = PermissionBank.GroupOperationPermission(groupname, "member", "add");
            var user_actor = HttpContext.Items["actor"] as User;
            if (user_actor.HasPermission(permission) != true)
            {
                throw new AuthenticateFailedException("not has enough permission when trying to add a member to a group.");
            }
            var user = _databaseService.Users.FirstOrDefault(t => t.Username == addGroupMemberRequestModel.UserName);
            if (user == null)
            {
                throw new UserNotExistException("The username you enter does not exist actually  when trying to add a grouptouser");
            }
            var grouptouser = _databaseService.GroupsToUsersRelation.FirstOrDefault(t => t.Group.GroupName == group.GroupName && t.User.Username == user.Username);
            if (grouptouser != null)
            {
                throw new GroupToUserAlreadyExistException("Grouptouser already exists when trying to add a grouptouser");
            }

            grouptouser = new GroupToUser();
            grouptouser.Group = group;
            grouptouser.GroupId = group.Id;
            grouptouser.User = user;
            grouptouser.UserId = user.Id;
            _databaseService.GroupsToUsersRelation.Add(grouptouser);
            _databaseService.SaveChanges();

            return Ok(new GroupMemberAddResultModel(group, user));
        }
    }
}
