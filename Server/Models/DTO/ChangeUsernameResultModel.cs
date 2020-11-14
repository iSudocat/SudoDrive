using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.Entities;

namespace Server.Models.DTO
{
    public class ChangeUsernameResultModel
    {
        public User User { get; private set; }

        public ChangeUsernameResultModel(User user)
        {
            User = user;
        }

    }
}
