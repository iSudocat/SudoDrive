using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.Entities;

namespace Server.Models.VO
{
    public class CloseUserResultModel
    {
        public User User { get; private set; }

        public CloseUserResultModel(User user)
        {
            User = user;
        }
    }
}
