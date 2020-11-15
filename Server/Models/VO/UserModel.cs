using System;
using Server.Models.Entities;

namespace Server.Models.VO
{
    public class UserModel
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Nickname { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public UserModel(User user)
        {
            this.Id = user.Id;
            this.Username = user.Username;
            this.Nickname = user.Nickname;
            this.CreatedAt = user.CreatedAt;
            this.UpdatedAt = user.UpdatedAt;
        }

    }
}