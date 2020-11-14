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
    [Route("api/auth/updateusername")]
    [ApiController]
    [NeedPermission(PermissionBank.UserAuthUpdateUsername)]
    public class UpdateUsernameController:AbstractController
    {
        private IDatabaseService _databaseService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseService">通过依赖注入获得数据库对象</param>
        public UpdateUsernameController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// 自己更改用户名
        /// </summary>
        /// <param name="newUsername"></param>
        /// <returns></returns>
        public IActionResult UpdateUsername(string newUsername)
        {
            if (!Regex.IsMatch(newUsername, @"^[a-zA-Z0-9-_]{4,16}$"))
            {
                throw new UsernameInvalidException("The username you enter is invalid when trying to change it.");
            }
            if(_databaseService.Users.FirstOrDefault(t=>t.Username==newUsername)!=null)
            {
                throw new UsernameDuplicatedException("Username duplicated.");

            }
            var user = HttpContext.Items["actor"] as User;
            var user_db = _databaseService.Users.FirstOrDefault(testc => testc.Username == user.Username);
            user_db.Username = newUsername;
            //缺少对User的UpdateAt的修改
            _databaseService.SaveChanges();
            return Ok(new UpdateUsernameResultModel(user_db));
            

        }
    }
}
