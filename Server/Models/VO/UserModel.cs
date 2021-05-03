using System;
using Server.Models.Entities;

namespace Server.Models.VO
{
    public class UserModel
    {
        public long Id { get; init; }

        public string Username { get; init; }

        public string Nickname { get; init; }

        public DateTime CreatedAt { get; init; }

        public DateTime UpdatedAt { get; init; }

        public int? Status { get; init; }

        public UserModel(User user)
        {
            this.Id = user.Id;
            this.Username = user.Username;
            this.Nickname = user.Nickname;
            this.CreatedAt = user.CreatedAt;
            this.UpdatedAt = user.UpdatedAt;
            this.Status = user.Status;
        }

    }
}