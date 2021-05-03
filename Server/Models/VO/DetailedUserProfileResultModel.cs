using System.Collections.Generic;
using Server.Models.Entities;

namespace Server.Models.VO
{
    public class DetailedUserProfileResultModel
    {
        public UserModel User { get; private init; }

        public List<GroupModel> Groups { get; private init; }

        public DetailedUserProfileResultModel(User user)
        {
            this.User = user.ToVO();

            if (user.GroupToUser == null) return;

            Groups = new List<GroupModel>();
            foreach (var t in user.GroupToUser)
            {
                Groups.Add(t.Group.ToVO());
            }
        }

    }
}