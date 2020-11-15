using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.Entities;
using Server.Models.VO;
using Server.Services;
using Server.Models.DTO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Server.Controllers.UserProfile
{
    [Route("api/user/{username}")]
    [ApiController]
    [NeedPermission(PermissionBank.UserProfileUpdateBasic)]
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
        public IActionResult UpdateProfile([FromBody] UserProfileUpdateRequestModel updateProfileRequestModel, string username)
        {
            if (!(HttpContext.Items["actor"] is User loginUser))
            {
                throw new UnexpectedException();
            }

            // 检索被操作的用户名
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9-_]{4,16}$"))
            {
                throw new UsernameInvalidException("The username given is invalid.");
            }
            var beingOperator =
                _databaseService.Users.FirstOrDefault(s => s.Username == username);
            if (beingOperator == null)
            {
                throw new UserNotExistException("User given is not exist.");
            }

            // 是否为自己更新
            var isSelf = loginUser.Id == beingOperator.Id;

            // 管理员
            var actorIsAdmin = loginUser.HasPermission(PermissionBank.UserProfileAdminUpdate);

            // 如果不是给自己更新信息就检查一下管理员权限
            if (!isSelf && actorIsAdmin != true)
            {
                throw new UnauthenticatedException();
            }

            // 更新昵称
            if (updateProfileRequestModel.Nickname != null)
            {

                if (updateProfileRequestModel.Nickname.Length < 4 || updateProfileRequestModel.Nickname.Length > 16)
                {
                    throw new NicknameInvalidException("The new nickname is invalid.");
                }

                beingOperator.Nickname = updateProfileRequestModel.Nickname;
            }

            if (updateProfileRequestModel.NewPassword != null)
            {
                // 旧密码
                var oldpw = updateProfileRequestModel.OldPassword;

                if (isSelf && actorIsAdmin != true || isSelf && actorIsAdmin == true && oldpw == null)
                {
                    // 不是管理员 自己更新自己密码的时候
                    // 是管理员 自己更新自己密码的时候携带上了旧的密码

                    if (BCrypt.Net.BCrypt.Verify(updateProfileRequestModel.OldPassword, beingOperator.Password))
                    {
                        beingOperator.Password = BCrypt.Net.BCrypt.HashPassword(updateProfileRequestModel.NewPassword);
                    }
                    else
                    {
                        throw new AuthenticateFailedException("The old password is not correct!");
                    }
                }
                else
                {
                    // 是管理员 更新密码的时候
                    beingOperator.Password = BCrypt.Net.BCrypt.HashPassword(updateProfileRequestModel.NewPassword);
                }

            }

            // 管理员
            if (actorIsAdmin == true && !isSelf)
            {
                if (updateProfileRequestModel.Status != null) {
                    beingOperator.Status = (int)updateProfileRequestModel.Status;
                }
            }

            _databaseService.SaveChanges();
            return Ok(new UserProfileUpdateResultModel(beingOperator));
        }
    }
}
