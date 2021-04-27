using Server.Models.Entities;

namespace Server.Models.VO
{
    public class RegisterResultModel
    {
        public UserModel User { get; private set; }

        public RegisterResultModel(User user)
        {
            User = user.ToVO();
        }
    }
}