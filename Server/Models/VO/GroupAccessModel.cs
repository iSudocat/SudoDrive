using System;
using Server.Models.Entities;
using Server.Libraries;

namespace Server.Models.VO
{
    public class GroupAccessModel
    {
        public long Id { get; private init; }
        public string GroupName { get; private init; }
        public bool? AddAccess { get; private init;}
        public bool? RemoveAccess { get; private init;}

        public DateTime CreatedAt { get; private init; }

        public DateTime UpdatedAt { get; private init; }

        public GroupAccessModel(Group group, User user)
        {
            this.Id = group.Id;
            this.GroupName = group.GroupName;
            this.CreatedAt = group.CreatedAt;
            this.UpdatedAt = group.UpdatedAt;

            string addPermission = PermissionBank.GroupOperationPermission(group.GroupName, "member", "add");
            if (user.HasPermission(addPermission) != true) {
                this.AddAccess = false;
            } else {
                this.AddAccess = true;
            }
            
            string removePermission = PermissionBank.GroupOperationPermission(group.GroupName, "member", "remove");
            if (user.HasPermission(removePermission) != true) {
                this.RemoveAccess = false;
            } else {
                this.RemoveAccess = true;
            }
        }
    }
}