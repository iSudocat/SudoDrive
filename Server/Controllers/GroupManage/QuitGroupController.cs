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
    [Route("api/group/quit")]
    [ApiController]
    [NeedPermission(PermissionBank.GroupManageGroupQuit)]
    public class QuitGroupController : AbstractController
    {
        private IDatabaseService _databaseService;
        public QuitGroupController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        //it is called id here, but it is groupname actually
        [HttpPost]
        public IActionResult QuitGroup([FromBody] QuitGroupRequestModel quitGroupRequestModel)
        {
            var group = _databaseService.Groups.FirstOrDefault(t => t.GroupName == quitGroupRequestModel.GroupName);
            if (group == null)
            {
                throw new GroupNotExistException("The groupname you enter does not exsit actually when trying to quit.");
            }
            var user = HttpContext.Items["actor"] as User;
            var grouptouser = _databaseService.GroupsToUsersRelation.FirstOrDefault(t => t.Group.GroupName == quitGroupRequestModel.GroupName&&t.UserId==user.Id);
            if (grouptouser==null)
            {
                throw new GroupToUserNotExistException("The user is not in the group at present.");
            }
            _databaseService.GroupsToUsersRelation.Remove(grouptouser);
            _databaseService.SaveChanges();

            return Ok(new QuitGroupResultModel(group.Id,user.Id));
        }
    }
}
