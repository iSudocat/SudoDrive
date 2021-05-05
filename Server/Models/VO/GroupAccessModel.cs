using Server.Models.Entities;
using Server.Libraries;

namespace Server.Models.VO
{
    public class GroupAccessModel : GroupModel
    {
        public bool CanAddMember { get; private init; }
        public bool CanRemoveMember { get; private init; }


        public GroupAccessModel(Group group, User user) : base(group)
        {
            string addPermission = PermissionBank.GroupOperationPermission(group.GroupName, "member", "add");
            if (user.HasPermission(addPermission) != true)
            {
                this.CanAddMember = false;
            }
            else
            {
                this.CanAddMember = true;
            }

            string removePermission = PermissionBank.GroupOperationPermission(group.GroupName, "member", "remove");
            if (user.HasPermission(removePermission) != true)
            {
                this.CanRemoveMember = false;
            }
            else
            {
                this.CanRemoveMember = true;
            }
        }
    }
}