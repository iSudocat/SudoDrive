using System.Collections.Generic;
using Server.Models.Entities;
using Server.Models.VO;

namespace Server.Models.DTO
{
    public class UpdateProfileResultModel
    {
        public UserModel User { get; private set; }

        public List<GroupModel> Groups { get; private set; }

        public UpdateProfileResultModel(User user)
        {
            this.User = user.ToVO();

            Groups = new List<GroupModel>();
            foreach (var t in user.GroupToUser)
            {
                Groups.Add(t.Group.ToVO());
            }
        }

    }
}
