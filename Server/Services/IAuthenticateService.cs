using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.VO;

namespace Server.Services
{
    public interface IAuthenticateService
    {
        bool IsAuthenticated(LoginRequestModel requestModel, out string token);
    }
}
