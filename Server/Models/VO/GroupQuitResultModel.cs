using Server.Models.Entities;

namespace Server.Models.VO
{
    public class GroupQuitResultModel
    {
        public GroupModel Group { get; private init; }
        public UserModel User { get; private init; }
        public GroupQuitResultModel(Group group, User user)
        {
            Group = group.ToVO();
            User = user.ToVO();
        }
    }
}