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

        public GetAttributesResultModel(long id, string username, string nickname, ICollection<GroupToUser> grouptouser, DateTime createdat,DateTime updatedat)
        {
            Id = id;
            Username = username;
            Nickname = nickname;
            GroupToUser = grouptouser;
            CreatedAt = createdat;
            UpdatedAt = updatedat;
        }

    }
}
