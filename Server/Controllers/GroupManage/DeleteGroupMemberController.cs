using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.DTO;
using Server.Models.Entities;
using Server.Models.VO;
using Server.Services;
using System.Linq;
using System.Text.RegularExpressions;

namespace Server.Controllers.GroupManage
{
    [Route("api/group/{groupname}/member")]
    [ApiController]
    [NeedPermission(PermissionBank.GroupManageGroupMemberRemoveBasic)]
    public class DeleteGroupMemberController : AbstractController
    {
        private IDatabaseService _databaseService;
        public DeleteGroupMemberController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpDelete]
        public IActionResult DeleteGroupMember([FromBody] DeleteGroupMemberRequestModel deleteGroupMemberRequestModel,string groupname)
        {
            if (!Regex.IsMatch(groupname, @"^[a-zA-Z0-9-_]{4,16}$"))
            {
                throw new GroupnameInvalidException("The groupname you enter is invalid when trying to delete a member from it.");
            }
            string permission = PermissionBank.GroupOperationPermission(groupname, "member", "remove");
            var user_actor = HttpContext.Items["actor"] as User;
            if (user_actor.HasPermission(permission) != true)
            {
                throw new AuthenticateFailedException("not has enough permission when trying to delete a member from a group.");
            }
            var group = _databaseService.Groups.FirstOrDefault(t => t.GroupName == groupname);
            if (group == null)
            {
                throw new GroupNotExistException("The groupname you enter does not exsit actually when trying to delete a grouptouser.");
            }
            var user = _databaseService.Users.FirstOrDefault(t => t.Username == deleteGroupMemberRequestModel.UserName);
            if (user == null)
            {
                throw new UserNotExistException("The username you enter does not exist actually  when trying to delete a grouptouser");
            }
            var grouptouser = _databaseService.GroupsToUsersRelation.FirstOrDefault(t => t.Group.GroupName == group.GroupName && t.User.Username==user.Username);
            if (grouptouser == null)
            {
                throw new GroupToUserNotExistException("The user is not in the group at present when deleting by another user.");
            }
            _databaseService.GroupsToUsersRelation.Remove(grouptouser);
            _databaseService.SaveChanges();

            return Ok(new GroupMemberRemoveResultModel(group,user));
        }
    }
}
