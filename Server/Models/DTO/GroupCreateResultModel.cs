using Server.Models.Entities;

namespace Server.Models.DTO
{
    public class GroupCreateResultModel
    {
        public Group Group { get; private set; }

        public GroupCreateResultModel( Group group)
        {
            this.Group = group;
        }
    }
}
