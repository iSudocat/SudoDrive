using Server.Models.Entities;

namespace Server.Models.VO
{
    public class GroupCreateResultModel
    {
        public GroupModel Group { get; init; }

        public GroupCreateResultModel(Group group)
        {
            this.Group = group.ToVO();
        }
    }
}
