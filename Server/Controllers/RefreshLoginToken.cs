using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.DTO;
using Server.Models.Entities;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/profile/refreshlogintoken")]
    [ApiController]
    [NeedPermission(PermissionBank.UserAuthRefresh)]
    public class RefreshLoginToken : Controller
    {
        private readonly IAuthenticateService _authService;

        public RefreshLoginToken(IAuthenticateService authService)
        {
            this._authService = authService;
        }
        
        public IActionResult GetNewToken()
        {
            if (!(HttpContext.Items["actor"] is User loginUser))
            {
                throw new UnexpectedException();
            }

            var token = _authService.GetNewToken(loginUser);
            return Ok(new LoginResultModel(loginUser.Username, token));
        }
    }
}
