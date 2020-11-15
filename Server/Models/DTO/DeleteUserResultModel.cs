using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.Entities;

namespace Server.Models.DTO
{
    public class DeleteUserResultModel
    {
        public long DeletedUserId { get; private set; }
        public string DeletedUserUsername { get;private set; }
        public DeleteUserResultModel(long deletedUserId,string deletedUserUsername)
        {
            DeletedUserId = deletedUserId;
            DeletedUserUsername = deletedUserUsername;
        }
    }
}
