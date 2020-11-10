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
using System.Threading.Tasks;

namespace Server.Controllers.GroupManage
{
    [Route("api/group/{groupname}/member")]
    [ApiController]
    [NeedPermission(PermissionBank.GroupManageGroupMemberDelete)]
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
            string permission = $"groupmanager.group.operation.{groupname}.member.delete";
            var user_actor = HttpContext.Items["actor"] as User;
            var user_actor_db = _databaseService.Users.Find(user_actor.Id);
            if (!(bool)user_actor_db.HasPermission(permission))
            {
                throw new AuthenticateFailedException("not has enough permission when trying to delete a member from a group.");
            }
            var group = _databaseService.Groups.FirstOrDefault(t => t.GroupName == deleteGroupMemberRequestModel.GroupName);
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

            return Ok(new DeleteGroupMemberResultModel(group,user));
        }
    }
}
