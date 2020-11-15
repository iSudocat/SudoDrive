using Server.Models.Entities;

namespace Server.Models.DTO
{
    public class UserProfileResultModel : CommonUserProfileResultModel
    {
        public UserProfileResultModel(User user) : base(user)
        {
        }
    }
}
