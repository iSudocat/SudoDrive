using Microsoft.AspNetCore.Mvc;
using Server.Models.Entities;
using Server.Models.VO;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Server.Libraries;
using Server.Middlewares;
using Server.Exceptions;

namespace Server.Controllers
{
    [Route("api/register")]
    [ApiController]
    [AllowAnonymous]
    [NeedPermission(PermissionBank.UserAuthRegister)]
    public class RegisterController : Controller
    {
        private IDatabaseService _databaseService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseService">通过依赖注入获得数据库对象</param>
        public RegisterController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpPost]
        public ActionResult<string> Register([FromBody] RegisterRequestModel registerRequestModel)
        {
            if (_databaseService.Users.FirstOrDefault(t => t.Username == registerRequestModel.Username) == null)
            {
                throw new UsernameDuplicatedException("Username duplicated.");
            }

            User user = new User();
            user.Username = registerRequestModel.Username;
            user.Password = BCrypt.Net.BCrypt.HashPassword(registerRequestModel.Password);
            _databaseService.Users.Add(user);
            _databaseService.SaveChanges();
            return Ok();
        }

    }
}
