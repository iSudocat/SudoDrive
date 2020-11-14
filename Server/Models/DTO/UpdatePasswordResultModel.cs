using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.Entities;

namespace Server.Models.DTO
{
    public class UpdatePasswordResultModel
    {
        public User User { get; private set; }

        public UpdatePasswordResultModel(User user)
        {
            User = user;
        }

    }
}
