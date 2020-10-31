using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.Entities;
using Server.Models.VO;

namespace Server.Services.Implements
{
    public class UserService : IUserService
    {
        public bool IsValid(LoginRequestModel req, out User loginUser)
        {
            // TODO 用户验证
            loginUser = new User();
            loginUser.Id = 1;
            loginUser.Username = req.Username;
            return true;
        }
    }
}
