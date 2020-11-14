using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.Entities;

namespace Server.Models.DTO
{
    public class GetAttributesOfOthersResultModel
    {
        public long Id { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public ICollection<GroupToUser> GroupToUser { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public GetAttributesOfOthersResultModel(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Password = user.Password;
            GroupToUser = user.GroupToUser;
            CreatedAt = user.CreatedAt;
            UpdatedAt = user.UpdatedAt;
        }

    }
}
