using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Models.DTO;
using Server.Models.VO;
using Server.Services;


namespace Server.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IAuthenticateService _authService;
        public LoginController(IAuthenticateService authService)
        {
            this._authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] LoginRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Request");
            }

            string token;
            if (_authService.IsAuthenticated(requestModel, out token))
            {
                return Ok(new LoginResultModel(requestModel.Username, token));
            }

            return BadRequest("Invalid Request");

        }
    }
}
