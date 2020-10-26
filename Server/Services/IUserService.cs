using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.VO;

namespace Server.Services
{
    public interface IUserService
    {
        bool IsValid(LoginRequestModel req);
    }
}
