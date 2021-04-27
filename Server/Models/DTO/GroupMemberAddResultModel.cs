using Server.Models.Entities;
using Server.Models.VO;

namespace Server.Models.DTO
{
    public class GroupMemberAddResultModel
    {
        public GroupModel Group { get; init; }
        public UserModel User { get; init; }
        
        public GroupMemberAddResultModel(Group group,User user)
        {
            Group = group.ToVO();
            User = user.ToVO();
        }
    }
}
