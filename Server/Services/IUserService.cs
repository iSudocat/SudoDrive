using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.DTO;
using Server.Models.Entities;
using Server.Models.VO;

namespace Server.Services
{
    public interface IUserService
    {
        public bool IsValid(LoginRequestModel req, out User loginUser);
    }
}
