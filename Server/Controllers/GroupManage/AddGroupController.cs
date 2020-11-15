using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.Entities;
using Server.Models.VO;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.DTO;

namespace Server.Controllers.GroupManage
{
    [Route("api/group")]
    [ApiController]
    [NeedPermission(PermissionBank.GroupManageGroupCreateBasic)]
    public class AddGroupController : AbstractController
    {
        private IDatabaseService _databaseService;
        public AddGroupController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpPost]
        public IActionResult AddGroup([FromBody] GroupCreateRequestModel addGroupRequestModel)
        {
            //use groupname to identify group,because the id is invisible to user
            if (_databaseService.Groups.FirstOrDefault(t => t.GroupName == addGroupRequestModel.GroupName) != null)
            {
                throw new GroupnameDuplicatedException("Groupname duplicated.");
            }
            //initialize new group and save it to database
            Group group = new Group();
            group.GroupName = addGroupRequestModel.GroupName;
            _databaseService.Groups.Add(group);
            //obtain the user
            var user = HttpContext.Items["actor"] as User;
            var user_db = _databaseService.Users.Find(user.Id);
            
            //initialize grouptouser and save it to database
            GroupToUser groupToUser = new GroupToUser();
            groupToUser.Group = group;
            groupToUser.GroupId = group.Id;
            groupToUser.User = user_db;
            groupToUser.UserId = user_db.Id;
            _databaseService.GroupsToUsersRelation.Add(groupToUser);

            // initial group permission to the new group
            _databaseService.GroupsToPermissionsRelation.Add(new GroupToPermission()
            {
                Group = group,
                GroupId = group.Id,
                Permission = PermissionBank.GroupOperationPermission(group.GroupName, "member", "add")
            });

            _databaseService.GroupsToPermissionsRelation.Add(new GroupToPermission()
            {
                Group = group,
                GroupId = group.Id,
                Permission = PermissionBank.GroupOperationPermission(group.GroupName, "member", "remove")
            });

            //find the grouptouser in the database
            //below is how to input parameters when the entity has composite key values:
            //"The ordering of composite key values is as defined in the EDM, which is in turn as defined in the designer, by the Code First fluent API, or by the DataMember attribute."
            var groupToUser_db = _databaseService.GroupsToUsersRelation.Find(groupToUser.GroupId, groupToUser.UserId);

            //waiting for adding permissions for the group
            
            _databaseService.SaveChanges();
            
            return Ok(new GroupCreateResultModel(group));
        }
    }
}