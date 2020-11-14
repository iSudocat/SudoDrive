using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.Entities;
using Server.Models.VO;
using Server.Services;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using Server.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Server.Controllers.UserProfile
{
    [Route("api/user/{username}")]
    [ApiController]
    [NeedPermission(PermissionBank.UserAuthGetAttributes)]
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
                    "The username you enter is invalid when trying to get user's attributes.");
            }

            var user_db = _databaseService.Users.FirstOrDefault(t => t.Username == username);
            if (user_db == null)
            {
                throw new UserNotExistException("Username Does Not Exist when trying to get user's attributes.");
            }

            string permission = PermissionBank.UserOperationPermission(username, "attribute", "get");
            var user_actor = HttpContext.Items["actor"] as User;
            if (!(bool) user_actor.HasPermission(permission))
            {
                throw new AuthenticateFailedException(
                    "not has enough permission when trying to get other user's attributes.");
            }

            return Ok(new GetAttributesResultModel(user_db));
        }
    }
}
