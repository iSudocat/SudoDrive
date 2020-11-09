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
    [Route("api/groupmanage/byanotheruser")]
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
            var grouptouser = _databaseService.GroupsToUsersRelation.FirstOrDefault(t => t.Group.GroupName == deleteGroupMemberRequestModel.GroupName && t.User.Username==deleteGroupMemberRequestModel.UserName);
            if (grouptouser == null)
            {
                throw new GroupToUserNotExistException("the user is not in the group at present when deleting by another user.");
            }
            _databaseService.GroupsToUsersRelation.Remove(grouptouser);
            _databaseService.SaveChanges();

            return Ok(new DeleteGroupMemberResultModel(deleteGroupMemberRequestModel.GroupName,deleteGroupMemberRequestModel.UserName));
        }
    }
}
