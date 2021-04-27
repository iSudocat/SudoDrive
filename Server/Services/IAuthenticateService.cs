using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.DTO;
using Server.Models.Entities;
using Server.Models.VO;

namespace Server.Services
{
    public interface IAuthenticateService
    {
        public string GetNewToken(User loginUser);

        public bool IsAuthenticated(LoginRequestModel requestModel, out string token, out User loginUser);
    }
}
