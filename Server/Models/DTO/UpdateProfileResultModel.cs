using Server.Models.Entities;

namespace Server.Models.DTO
{
    public class UpdateProfileResultModel
    {
        public long UpdateUserId { get; private set; }

        public string UpdateUserUsername { get; private set; }

        public UpdateProfileResultModel(long updateUserId,string updateUserUsername)
        {
            UpdateUserId = updateUserId;
            UpdateUserUsername = updateUserUsername;
        }

    }
}
