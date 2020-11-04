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

        private readonly User _loginUser;

        public RefreshLoginToken(IAuthenticateService authService)
        {
            this._authService = authService;

            _loginUser = HttpContext.Items["actor"] as User;
            if (_loginUser == null)
            {
                throw new UnexpectedException();
            }
        }
        
        public IActionResult GetNewToken()
        {
            var token = _authService.GetNewToken(_loginUser);
            return Ok(new LoginResultModel(_loginUser.Username, token));
        }
    }
}
