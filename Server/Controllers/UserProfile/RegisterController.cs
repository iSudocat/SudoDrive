using System.Linq;
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
            if (_databaseService.Users.FirstOrDefault(t => t.Username == registerRequestModel.Username) != null)
            {
                throw new UsernameDuplicatedException("Username duplicated.");
            }

            User user = new User();
            user.Username = registerRequestModel.Username;
            user.Password = BCrypt.Net.BCrypt.HashPassword(registerRequestModel.Password);
            user.Nickname = registerRequestModel.Nickname;
            _databaseService.Users.Add(user);
            _databaseService.SaveChanges();

            return Ok(new RegisterResultModel(user));
        }
    }
}
