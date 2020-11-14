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
    [Route("api/auth/{username}")]
    [ApiController]
    [NeedPermission(PermissionBank.UserAuthUpdateProfile)]
    public class UpdateProfileController : AbstractController
    {
        private IDatabaseService _databaseService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseService">通过依赖注入获得数据库对象</param>
        public UpdateProfileController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// 改变用户信息
        /// </summary>
        /// <param name="updateProfileRequestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateProfile([FromBody] UpdateProfileRequestModel updateProfileRequestModel)
        {
            //验证username合法且存在
            if (!Regex.IsMatch(updateProfileRequestModel.Username, @"^[a-zA-Z0-9-_]{4,16}$"))
            {
                throw new UsernameInvalidException("The username you enter is invalid when trying to update password.");
            }

            var user_db =
                _databaseService.Users.FirstOrDefault(testc => testc.Username == updateProfileRequestModel.Username);
            if (user_db == null)
            {
                throw new UserNotExistException("Username Does Not Exist when trying to update password.");
            }

            //update nickname
            if (updateProfileRequestModel.Nickname != null)
            {
                if (!Regex.IsMatch(updateProfileRequestModel.Nickname, @"^[a-zA-Z0-9-_]{4,16}$"))
                {
                    throw new NicknameInvalidException(
                        "The nickname you enter is invalid when trying to  change nickname.");
                }

                if (user_db.Nickname == updateProfileRequestModel.Nickname)
                {
                    throw new NicknameDuplicatedException(
                        "The nickname you enter is duplicated when trying to update nickname");
                }

                string permission =
                    PermissionBank.UserOperationPermission(updateProfileRequestModel.Username, "attribute", "update");
                var user_actor = HttpContext.Items["actor"] as User;
                if (!(bool) user_actor.HasPermission(permission))
                {
                    throw new AuthenticateFailedException("not has enough permission when trying to update nickname.");
                }

                user_db.Nickname = updateProfileRequestModel.Nickname;
            }

            //update password
            if (updateProfileRequestModel.NewPassword != null)
            {
                if (!Regex.IsMatch(updateProfileRequestModel.NewPassword, (@"^[^\n\r]{8,}$")))
                {
                    throw new PasswordNotMatchException(
                        "The password you enter is invalid when trying to update password.");
                }

                string permission =
                    PermissionBank.UserOperationPermission(updateProfileRequestModel.Username, "attribute", "update");
                var user_actor = HttpContext.Items["actor"] as User;
                if (!(bool) user_actor.HasPermission(permission))
                {
                    throw new AuthenticateFailedException("not has enough permission when trying to update password.");
                }

                if (BCrypt.Net.BCrypt.Verify(updateProfileRequestModel.OldPassword, user_db.Password))
                {
                    user_db.Password = BCrypt.Net.BCrypt.HashPassword(updateProfileRequestModel.NewPassword);
                }
                else
                {
                    throw new AuthenticateFailedException("The old password is not correct!");
                }
            }

            //update photo


            //保存更改并返回结果
            _databaseService.SaveChanges();
            return Ok(new UpdateProfileResultModel(user_db));
        }
    }
}
