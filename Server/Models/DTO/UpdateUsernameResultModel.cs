using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.Entities;

namespace Server.Models.DTO
{
    public class UpdateUsernameResultModel
    {
        public User User { get; private set; }
        public UpdateUsernameResultModel(DeleteUserResultModel user)
        {
            User = user;
        }
    }
}
