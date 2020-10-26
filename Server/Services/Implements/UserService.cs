using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.VO;

namespace Server.Services.Implements
{
    public class UserService : IUserService
    {
        public bool IsValid(LoginRequestModel req)
        {
            // TODO 用户验证
            return true;
        }
    }
}
