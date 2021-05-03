using Server.Models.Entities;

namespace Server.Models.VO
{
    public class GroupDeleteResultModel
    {
        public GroupModel Group { get; private init; }
        public GroupDeleteResultModel(Group group)
        {
            Group = group.ToVO();
        }
    }
}
