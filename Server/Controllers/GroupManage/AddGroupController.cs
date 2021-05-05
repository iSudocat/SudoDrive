using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.Entities;
using Server.Models.VO;
using Server.Services;
using System.Linq;
using Server.Models.DTO;
using EntityFile = Server.Models.Entities.File;

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

            //initialize grouptouser and save it to database
            GroupToUser groupToUser = new GroupToUser();
            groupToUser.Group = group;
            groupToUser.GroupId = group.Id;
            groupToUser.User = user;
            groupToUser.UserId = user.Id;
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
            
            _databaseService.GroupsToPermissionsRelation.Add(new GroupToPermission()
            {
                Group = group,
                GroupId = group.Id,
                Permission = PermissionBank.GroupOperationPermission(group.GroupName, "member", "list")
            });

            //find the grouptouser in the database
            //below is how to input parameters when the entity has composite key values:
            //"The ordering of composite key values is as defined in the EDM, which is in turn as defined in the designer, by the Code First fluent API, or by the DataMember attribute."
            // var groupToUser_db = _databaseService.GroupsToUsersRelation.Find(groupToUser.GroupId, groupToUser.UserId);

            //waiting for adding permissions for the group

            var groupDirectory = EntityFile.CreateDirectoryRecord(group.GroupName, "/groups", $"/groups/{group.GroupName}", user);
            _databaseService.Files.Add(groupDirectory);
            
            _databaseService.SaveChanges();
            
            return Ok(new GroupCreateResultModel(group));
        }
    }
}