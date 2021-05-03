using System.Collections.Generic;
using Server.Models.Entities;

namespace Server.Models.VO
{
    public class LoginResultModel
    {
        public string Username { get; private init; }

        public string Token { get; private init; }

        public UserModel User { get; private init; }

        public List<GroupModel> Groups { get; private init; }

        public LoginResultModel(string username, string token, User user)
        {
            this.Username = username;
            this.Token = token;

            this.User = user.ToVO();

            Groups = new List<GroupModel>();
            foreach (var t in user.GroupToUser)
            {
                Groups.Add(t.Group.ToVO());
            }
        }

    }
}
