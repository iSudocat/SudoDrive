using Server.Models.Entities;
using Server.Models.VO;

namespace Server.Models.DTO
{
    public class UpdateProfileResultModel
    {
        public UserModel User { get; private set; }

        public UpdateProfileResultModel(User user)
        {
            User = user.ToVO();
        }

    }
}
