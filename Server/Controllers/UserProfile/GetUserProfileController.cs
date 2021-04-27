using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Services;
using Server.Models.DTO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Server.Models.VO;

namespace Server.Controllers.UserProfile
{
    [Route("api/user/{username}")]
    [ApiController]
    [NeedPermission(PermissionBank.UserProfileBasic)]
    public class GetUserProfileController : AbstractController
    {
        private IDatabaseService _databaseService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseService">通过依赖注入获得数据库对象</param>
        public GetUserProfileController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// 获得其他用户的信息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAttributes(string username)
        {
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9-_]{4,16}$"))
            {
                throw new UsernameInvalidException(
                    "The username given is invalid.");
            }

            var user_db = _databaseService.Users
                .Include(s => s.GroupToUser)
                .ThenInclude(s => s.Group)
                .FirstOrDefault(t => t.Username == username);

            if (user_db == null)
            {
                throw new UserNotExistException("The username given is not found.");
            }

            return Ok(new UserProfileResultModel(user_db));
        }
    }
}
