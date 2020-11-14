using Server.Models.Entities;

namespace Server.Models.DTO
{
    public class UpdateProfileResultModel
    {
        public User User { get; private set; }

        public UpdateProfileResultModel(User user)
        {
            User = user;
        }

    }
}
