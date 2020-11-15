using Server.Models.Entities;

namespace Server.Models.DTO
{
    public class UserProfileUpdateResultModel : CommonUserProfileResultModel
    {
        public UserProfileUpdateResultModel(User user) : base(user)
        {
        }
    }
}
