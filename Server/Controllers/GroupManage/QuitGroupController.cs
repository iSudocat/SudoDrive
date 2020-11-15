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
    [Route("api/group/{groupname}/quit")]
    [ApiController]
    [NeedPermission(PermissionBank.GroupManageGroupQuitBasic)]
    public class QuitGroupController : AbstractController
    {
        private IDatabaseService _databaseService;
        public QuitGroupController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpDelete]
        public IActionResult QuitGroup(string groupname)
        {
            if (!Regex.IsMatch(groupname, @"^[a-zA-Z0-9-_]{4,16}$"))
            {
                throw new GroupnameInvalidException("The groupname you enter is invalid when trying to quit.");
            }
            var group = _databaseService.Groups.FirstOrDefault(t => t.GroupName == groupname);
            if (group == null)
            {
                throw new GroupNotExistException("The groupname you enter does not exsit actually when trying to quit.");
            }
            var user = HttpContext.Items["actor"] as User;
            var grouptouser = _databaseService.GroupsToUsersRelation.FirstOrDefault(t => t.Group.GroupName == groupname&&t.UserId==user.Id);
            if (grouptouser==null)
            {
                throw new GroupToUserNotExistException("The user is not in the group at present.");
            }
            _databaseService.GroupsToUsersRelation.Remove(grouptouser);
            _databaseService.SaveChanges();

            return Ok(new QuitGroupResultModel(group,user));
        }
    }
}
