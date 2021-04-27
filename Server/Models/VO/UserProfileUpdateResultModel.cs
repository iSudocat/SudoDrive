using Server.Models.Entities;

namespace Server.Models.VO
{
    public class UserProfileUpdateResultModel : CommonUserProfileResultModel
    {
        public UserProfileUpdateResultModel(User user) : base(user)
        {
        }
    }
}
