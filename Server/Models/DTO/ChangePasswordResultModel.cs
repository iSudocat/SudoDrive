using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.Entities;


namespace Server.Models.DTO
{
    public class ChangePasswordResultModel
    {
        public User User { get; private set; }

        public ChangePasswordResultModel(User user)
        {
            User = user;
        }
    }
}
