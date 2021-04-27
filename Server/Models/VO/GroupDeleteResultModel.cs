using Server.Models.Entities;

namespace Server.Models.VO
{
    public class GroupDeleteResultModel
    {
        public GroupModel Group { get; private set; }
        public GroupDeleteResultModel(Group group)
        {
            Group = group.ToVO();
        }
    }
}
