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
    [Route("api/auth/{username}/changeusername")]
    [ApiController]
    [NeedPermission(PermissionBank.UserAuthChangeUsername)]
    public class ChangeUsernameController:AbstractController
    {
        private IDatabaseService _databaseService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseService">通过依赖注入获得数据库对象</param>
        public ChangeUsernameController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// 用户改变其他用户的用户名
        /// </summary>
        /// <param name="changeUsernameRequestModel"></param>
        /// <returns></returns>
        public IActionResult ChangeUsername([FromBody] ChangeUsernameRequestModel changeUsernameRequestModel)
        {
            if (!Regex.IsMatch(changeUsernameRequestModel.NewUsername, @"^[a-zA-Z0-9-_]{4,16}$"))
            {
                throw new UsernameInvalidException("The username you enter is invalid when trying to  change username.");
            }
            if (_databaseService.Users.FirstOrDefault(t => t.Username == changeUsernameRequestModel.NewUsername) != null)
            {
                throw new UsernameDuplicatedException("Username duplicated.");
            }
            string permission = PermissionBank.UserOperationPermission(changeUsernameRequestModel.OldUsername,"username","change");
            var user_actor = HttpContext.Items["actor"] as User;
            if (!(bool)user_actor.HasPermission(permission))
            {
                throw new AuthenticateFailedException("not has enough permission when trying to change username.");
            }
            var user_db = _databaseService.Users.FirstOrDefault(testc => testc.Username == changeUsernameRequestModel.OldUsername);
            user_db.Username = changeUsernameRequestModel.NewUsername;
            _databaseService.SaveChanges();
            return Ok(new ChangeUsernameResultModel(user_db));

        }

    }
}
