using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.Entities

namespace Server.Models.DTO
{
    public class DeleteUserResultModel
    {
        public User User { get; private set; }

        public DeleteUserResultModel(User user)
        {
            User = user;
        }
    }
}
