using Microsoft.AspNetCore.Mvc;
using Server.Models.Entities;
using Server.Services;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.VO;

namespace Server.Controllers
{
    [Route("api/auth/updatepassword")]
    [ApiController]
    [NeedPermission(PermissionBank.UserAuthUpdatePassword)]
    public class UpdatePasswordController: Controller
    {
        private IDatabaseService _databaseService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseService">通过依赖注入获得数据库对象</param>
        public UpdatePasswordController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="changePasswordRequestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordRequestModel changePasswordRequestModel)
        {
            var user= HttpContext.Items["actor"] as User;
            if (BCrypt.Net.BCrypt.Verify(changePasswordRequestModel.OldPassword, user.Password))
            {
                var user_db = _databaseService.Users.Find(user.Id);
                user_db.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordRequestModel.NewPassword);
                if (!user_db.PasswordValid())
                {
                    throw new InvalidPasswordException("new Password Pattern Invalid when updating password.");
                }
                _databaseService.SaveChanges();
            }
            else
            {
                throw new AuthenticateFailedException("The old password is not correct!");
            }
            return Ok();
        }
    }
}
