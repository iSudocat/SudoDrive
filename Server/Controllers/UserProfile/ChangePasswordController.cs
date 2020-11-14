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
    [Route("api/auth/{username}/changepassword")]
    [ApiController]
    [NeedPermission(PermissionBank.UserAuthChangePassword)]
    public class ChangePasswordController:AbstractController
    {
        private IDatabaseService _databaseService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseService">通过依赖注入获得数据库对象</param>
        public ChangePasswordController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        /// <summary>
        /// 用户改变其他用户的密码
        /// </summary>
        /// <param name="changePasswordRequestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ChangePassword([FromBody] ChangePasswordRequestModel changePasswordRequestModel)
        {
            if (!Regex.IsMatch(changePasswordRequestModel.Username, @"^[a-zA-Z0-9-_]{4,16}$"))
            {
                throw new UsernameInvalidException("The username you enter is invalid when trying to  change password.");
            }

            string permission = PermissionBank.UserOperationPermission(changePasswordRequestModel.Username, "password","update");
            var user_actor = HttpContext.Items["actor"] as User;
            if (!(bool)user_actor.HasPermission(permission))
            {
                throw new AuthenticateFailedException("not has enough permission when trying to change password.");
            }

            var user_db = _databaseService.Users.FirstOrDefault(testc => testc.Username == changePasswordRequestModel.Username);

            if (BCrypt.Net.BCrypt.Verify(changePasswordRequestModel.OldPassword, user_db.Password))
            {
               
                user_db.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordRequestModel.NewPassword);
                //缺少对User的UpdateAt的修改
                _databaseService.SaveChanges();
            }
            else
            {
                throw new AuthenticateFailedException("The old password is not correct!");
            }

            return Ok(new ChangePasswordResultModel(user_db));
        }

    }
}
