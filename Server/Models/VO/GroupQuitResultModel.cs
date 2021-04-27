using Server.Models.Entities;

namespace Server.Models.VO
{
    public class GroupQuitResultModel
    {
        public GroupModel Group { get; private set; }
        public UserModel User { get; private set; }
        public GroupQuitResultModel(Group group, User user)
        {
            Group = group.ToVO();
            User = user.ToVO();
        }
    }
}