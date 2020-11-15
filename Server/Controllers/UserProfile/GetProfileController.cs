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
    [NeedPermission(PermissionBank.UserAuthGetProfile)]
    public class GetProfileController : AbstractController
    {
        private IDatabaseService _databaseService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseService">通过依赖注入获得数据库对象</param>
        public GetProfileController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// 获得其他用户的信息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUserProfile(string username)
        {
            //判断用户名合法
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9-_]{4,16}$"))
            {
                throw new UsernameInvalidException(
                    "The username you enter is invalid when trying to get the its profile.");
            }
            //判断用户存在，并获取数据库中的该用户
            var user_db = _databaseService.Users.FirstOrDefault(t => t.Username == username);
            if (user_db == null)
            {
                throw new UserNotExistException("Username Does Not Exist when trying to get its profile.");
            }
            //判断权限
            string permission = PermissionBank.UserOperationPermission(username, "profile", "get");
            var user_actor = HttpContext.Items["actor"] as User;
            if (user_actor.HasPermission(permission) != true)
            {
                throw new AuthenticateFailedException(
                    "not has enough permission when trying to get other user's attributes.");
            }
            //执行查询
            long GotId = user_db.Id;
            string GotUsername = user_db.Username;
            ICollection<GroupToUser> GotGroupToUser = user_db.GroupToUser;
            DateTime GotCreatedAt = user_db.CreatedAt;
            DateTime GotUpdatedAt = user_db.UpdatedAt;
            string GotNickname = user_db.Nickname;
            //返回结果
            return Ok(new GetUserProfileControllerResultModel(GotId, GotUsername, GotGroupToUser, GotCreatedAt, GotUpdatedAt, GotNickname));
        }
    }
}
