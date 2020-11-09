using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.DTO;
using Server.Models.VO;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers.GroupManage
{
    [Route("api/group/member")]
    [ApiController]
    [NeedPermission(PermissionBank.GroupmanageDeletegroupmember)]
    public class DeleteGroupMemberController : AbstractController
    {
        private IDatabaseService _databaseService;
        public DeleteGroupMemberController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpDelete]
        public IActionResult DeleteGroupMember([FromBody] DeleteGroupMemberRequestModel deleteGroupMemberRequestModel)
        {
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

            return Ok(new DeleteGroupMemberResultModel(group.Id,user.Id));
        }
    }
}
