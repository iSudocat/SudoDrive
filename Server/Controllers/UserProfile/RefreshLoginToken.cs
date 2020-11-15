using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.DTO;
using Server.Services;
using Server.Models.Entities;

namespace Server.Controllers.UserProfile
{
    [Route("api/refreshlogintoken")]
    [ApiController]
    [NeedPermission(PermissionBank.UserAuthRefresh)]
    public class RefreshLoginToken : AbstractController
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
