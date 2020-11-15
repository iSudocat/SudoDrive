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
    [Route("api/{username}")]
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
        [HttpPatch]
        public IActionResult UpdateProfile([FromBody] UpdateProfileRequestModel updateProfileRequestModel)
        {
            //判断用户名合法
            if (!Regex.IsMatch(updateProfileRequestModel.Username, @"^[a-zA-Z0-9-_]{4,16}$"))
            {
                throw new UsernameInvalidException("The username you enter is invalid when trying to update its profile.");
            }
            //判断用户存在，并从数据库中获得该用户
            var user_db =
                _databaseService.Users.FirstOrDefault(testc => testc.Username == updateProfileRequestModel.Username);
            if (user_db == null)
            {
                throw new UserNotExistException("Username Does Not Exist when trying to update its profile.");
            }

            //用updateProfileRequestModel的属性是否为null来判断此次UpdateProfile是否修改了user的对应属性
            //为null则没有修改该属性，不为null则修改了该属性

            //update nickname
            if (updateProfileRequestModel.NewNickname != null)
            {
                //判断新别名合法
                if (!Regex.IsMatch(updateProfileRequestModel.NewNickname, @"^[a-zA-Z0-9-_]{4,16}$"))
                {
                    throw new NicknameInvalidException(
                        "The nickname you enter is invalid when trying to update nickname.");
                }
                //判断新别名不重复
                if (_databaseService.Users.FirstOrDefault(t => t.Nickname == updateProfileRequestModel.NewNickname) != null)
                {
                    throw new NicknameDuplicatedException(
                        "The nickname you enter is duplicated when trying to update nickname");
                }
                //判断权限
                string permission =
                    PermissionBank.UserOperationPermission(updateProfileRequestModel.Username, "profile", "update");
                var user_actor = HttpContext.Items["actor"] as User;
                if (user_actor.HasPermission(permission) != true )
                {
                    throw new AuthenticateFailedException("not has enough permission when trying to update nickname.");
                }
                //执行修改
                user_db.Nickname = updateProfileRequestModel.NewNickname;
            }

            //update password
            if (updateProfileRequestModel.NewPassword != null)
            {
                //判断新密码合法
                if (!Regex.IsMatch(updateProfileRequestModel.NewPassword, (@"^[^\n\r]{8,}$")))
                {
                    throw new PasswordNotMatchException(
                        "The password you enter is invalid when trying to update password.");
                }
                //判断权限
                string permission =
                    PermissionBank.UserOperationPermission(updateProfileRequestModel.Username, "profile", "update");
                var user_actor = HttpContext.Items["actor"] as User;
                if (user_actor.HasPermission(permission) != true)
                {
                    throw new AuthenticateFailedException("not has enough permission when trying to update password.");
                }
                //判断旧密码正确
                if (BCrypt.Net.BCrypt.Verify(updateProfileRequestModel.OldPassword, user_db.Password))
                {
                    //执行修改
                    user_db.Password = BCrypt.Net.BCrypt.HashPassword(updateProfileRequestModel.NewPassword);
                }
                else
                {
                    throw new AuthenticateFailedException("The old password is not correct!");
                }
            }
            //保存更改
            _databaseService.SaveChanges();
            long UpdateUserId = user_db.Id;
            string UpdateUserUsername = user_db.Username;
            //返回结果
            return Ok(new UpdateProfileResultModel(UpdateUserId, UpdateUserUsername));
        }
    }
}
