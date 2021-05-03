using Server.Models.Entities;

namespace Server.Models.VO
{
    public class UserProfileUpdateResultModel : DetailedUserProfileResultModel
    {
        public UserProfileUpdateResultModel(User user) : base(user)
        {
        }
    }
}
