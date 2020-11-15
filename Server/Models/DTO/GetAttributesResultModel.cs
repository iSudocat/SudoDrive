using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.Entities;

namespace Server.Models.DTO
{
    public class GetAttributesResultModel
    {
        public long Id { get; private set; }

        public string Username { get; private set; }

        public string Nickname { get; private set; }

        public ICollection<GroupToUser> GroupToUser { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public GetAttributesResultModel(User p)
        {
            Id = p.Id;
            Username = p.Username;
            Nickname = p.Nickname;
            GroupToUser = p.GroupToUser;
            CreatedAt = p.CreatedAt;
            UpdatedAt = p.UpdatedAt;
        }

    }
}
