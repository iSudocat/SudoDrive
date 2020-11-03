using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Middlewares;
using Server.Models;
using Server.Models.DTO;
using Server.Models.VO;
using Server.Services;


namespace Server.Controllers
{
    [Route("api/login")]
    [ApiController]
    [AllowAnonymous]
    [NeedPermission("user.login")]
    public class LoginController : Controller
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
                throw new AuthenticateFailedException("Password or Username is wrong.");
            }

            string token;
            if (_authService.IsAuthenticated(requestModel, out token))
            {
                return Ok(new LoginResultModel(requestModel.Username, token));
            }

            throw new UnexpectedException();

        }
    }
}
