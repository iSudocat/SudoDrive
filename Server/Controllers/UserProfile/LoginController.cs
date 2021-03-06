using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.DTO;
using Server.Models.Entities;
using Server.Models.VO;
using Server.Services;

namespace Server.Controllers.UserProfile
{
    [Route("api/login")]
    [ApiController]
    [AllowAnonymous]
    [NeedPermission(PermissionBank.UserAuthLogin)]
    public class LoginController : AbstractController
    {
        private readonly IAuthenticateService _authService;

        public LoginController(IAuthenticateService authService)
        {
            this._authService = authService;
        }

        [HttpPost]
        public ActionResult Login([FromBody] LoginRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidArgumentException();
            }

            string token;
            User user;
            if (_authService.IsAuthenticated(requestModel, out token, out user))
            {
                return Ok(new LoginResultModel(requestModel.Username, token, user));
            }

            throw new AuthenticateFailedException("Password or Username is wrong.");
        }
    }
}
