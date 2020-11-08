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

namespace Server.Controllers.GroupManage
{
    [Route("api/groupmanage")]
    [ApiController]
    [NeedPermission(PermissionBank.GroupmanageAddgroup)]
    public class AddGroupController : AbstractController
    {
        private IDatabaseService _databaseService;
        public AddGroupController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpPost]
        public IActionResult AddGroup([FromBody] AddGroupRequestModel addGroupRequestModel)
        {
            //use groupname to identify group,because the id is invisible to user
            if (_databaseService.Groups.FirstOrDefault(t => t.GroupName == addGroupRequestModel.GroupName) != null)
            {
                throw new GroupnameDuplicatedException("Groupname duplicated.");
            }
            //initialize new group and save it to database
            Group group = new Group();
            group.GroupName = addGroupRequestModel.GroupName;
            group.CreatedAt = DateTime.Now;
            group.UpdatedAt = group.CreatedAt;
            _databaseService.Groups.Add(group);
            //obtain the user
            var user = HttpContext.Items["actor"] as User;
            //find the group,user in the database
            var group_db = _databaseService.Groups.FirstOrDefault(t=>t.GroupName == group.GroupName);
            var user_db = _databaseService.Users.Find(user.Id);
            
            //initialize grouptouser and save it to database
            GroupToUser groupToUser = new GroupToUser();
            groupToUser.Group = group_db;
            groupToUser.GroupId = group_db.Id;
            groupToUser.User = user_db;
            groupToUser.UserId = user_db.Id;
            _databaseService.GroupsToUsersRelation.Add(groupToUser);

            //find the grouptouser in the database
            //below is how to input parameters when the entity has composite key values:
            //"The ordering of composite key values is as defined in the EDM, which is in turn as defined in the designer, by the Code First fluent API, or by the DataMember attribute."
            var groupToUser_db = _databaseService.GroupsToUsersRelation.Find(groupToUser.GroupId, groupToUser.UserId);

            group_db.GroupToUser.Add(groupToUser_db);
            user_db.GroupToUser.Add(groupToUser_db);

            group_db.UpdatedAt = DateTime.Now;
            user_db.UpdatedAt = group_db.UpdatedAt;

            //waiting for adding permissions for the group
            
            _databaseService.SaveChanges();
            
            return Ok();
        }
    }
}