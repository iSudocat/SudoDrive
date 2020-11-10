using Server.Models.Entities;

namespace Server.Models.DTO
{
    public class AddGroupResultModel
    {
        public Group Group { get; private set; }

        public AddGroupResultModel( Group group)
        {
            this.Group = group;
        }
    }
}
