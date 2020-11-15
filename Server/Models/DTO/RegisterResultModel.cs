using Server.Models.Entities;
using Server.Models.VO;

namespace Server.Models.DTO
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