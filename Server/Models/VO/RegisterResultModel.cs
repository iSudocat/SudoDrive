using Server.Models.Entities;

namespace Server.Models.VO
{
    public class RegisterResultModel
    {
        public UserModel User { get; private init; }

        public RegisterResultModel(User user)
        {
            User = user.ToVO();
        }
    }
}