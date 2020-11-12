using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
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
    [Route("api/group/{groupname}")]
    [ApiController]
    [NeedPermission(PermissionBank.GroupManageGroupDelete)]
    public class DeleteGroupController : AbstractController
    {
        private IDatabaseService _databaseService;
        public DeleteGroupController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpDelete]
        public IActionResult DeleteGroup(string groupname)
        {
            if (!Regex.IsMatch(groupname, @"^[a-zA-Z0-9-_]{4,16}$"))
            {
                throw new GroupnameInvalidException("The groupname you enter is invalid when trying to delete it.");
            }
            string permission = PermissionBank.GroupOperationPermission(groupname,"","delete");
            var user_actor = HttpContext.Items["actor"] as User;
            if (user_actor.HasPermission(permission) != true)
            {
                throw new AuthenticateFailedException("not has enough permission when trying to delete a group.");
            }
            //use groupname to identify group,because the id is invisible to user
            var group = _databaseService.Groups.FirstOrDefault(t => t.GroupName == groupname);
            if (group == null)
            {
                throw new GroupNotExistException("Groupname Does Not Exist when trying to delete group.");
            }
           
            var grouptouser_db = _databaseService.GroupsToUsersRelation.Where(t => t.Group.GroupName == group.GroupName);
            //try to update the updatetime of all the users which belongs to this group, but failed as below
            //var user_db = _databaseService.Users.Where(t => grouptouser_db.Contains(t.GroupToUser));
            var grouptopermission_db = _databaseService.GroupsToPermissionsRelation.Where(t => t.Group.GroupName == group.GroupName);
            _databaseService.GroupsToPermissionsRelation.RemoveRange(grouptopermission_db);
            _databaseService.GroupsToUsersRelation.RemoveRange(grouptouser_db);
            _databaseService.Groups.Remove(group);
            _databaseService.SaveChanges();
            return Ok(new DeleteGroupResultModel(group));
        }
    }
}
