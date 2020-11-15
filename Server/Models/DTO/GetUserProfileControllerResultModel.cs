using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.Entities;

namespace Server.Models.DTO
{
    public class GetUserProfileControllerResultModel
    {
        public long GotId { get; private set; }

        public string GotUsername { get; private set; }

        public ICollection<GroupToUser> GotGroupToUser { get; private set; }

        public DateTime GotCreatedAt { get; private set; }

        public DateTime GotUpdatedAt { get; private set; }

        public string GotNickname { get; private set; }

        public GetUserProfileControllerResultModel(long gotId,string gotUsername, ICollection<GroupToUser> gotGroupToUser, DateTime gotCreatedAt, DateTime gotUpdatedAt, string gotNickname)
        {
            GotId = gotId;
            GotUsername = gotUsername;
            GotGroupToUser = gotGroupToUser;
            GotCreatedAt = gotCreatedAt;
            GotUpdatedAt = gotUpdatedAt;
            GotNickname = gotNickname;
        }

    }
}
