using System.Collections.Generic;
using Server.Models.Entities;

namespace Server.Models.VO
{
    public class LoginResultModel
    {
        public string Username { get; private set; }

        public string Token { get; private set; }

        public UserModel User { get; private set; }

        public List<GroupModel> Groups { get; private set; }

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
