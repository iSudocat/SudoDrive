using System.Collections.Generic;
using Server.Models.Entities;

namespace Server.Models.VO
{
    public class DetailedUserProfileResultModel
    {
        public UserModel User { get; private init; }

        public List<GroupAccessModel> Groups { get; private init; }

        public DetailedUserProfileResultModel(User user)
        {
            this.User = user.ToVO();

            if (user.GroupToUser == null) return;

            Groups = new List<GroupAccessModel>();
            foreach (var t in user.GroupToUser)
            {
                Groups.Add(new GroupAccessModel(t.Group, user));
            }
        }

    }
}