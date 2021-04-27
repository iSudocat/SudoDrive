using Server.Models.Entities;
using Server.Models.VO;

namespace Server.Models.DTO
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
