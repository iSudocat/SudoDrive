using Server.Models.Entities;

namespace Server.Models.VO
{
    public class UserProfileResultModel : DetailedUserProfileResultModel
    {
        public UserProfileResultModel(User user) : base(user)
        {
        }
    }
}
