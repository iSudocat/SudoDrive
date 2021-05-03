using Server.Models.Entities;

namespace Server.Models.VO
{
    public class GroupMemberRemoveResultModel
    {
        public GroupModel Group { get; private init; }
        public UserModel User { get; private init; }
        public GroupMemberRemoveResultModel(Group group,User user)
        {
            Group = group.ToVO();
            User = user.ToVO();
        }
    }
}
