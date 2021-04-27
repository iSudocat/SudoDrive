using Server.Models.Entities;

namespace Server.Models.VO
{
    public class UserProfileResultModel : CommonUserProfileResultModel
    {
        public UserProfileResultModel(User user) : base(user)
        {
        }
    }
}
