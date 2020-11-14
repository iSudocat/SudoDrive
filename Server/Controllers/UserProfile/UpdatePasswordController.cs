using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.DTO;
using Server.Models.Entities;
using Server.Models.VO;
using Server.Services;

namespace Server.Controllers.UserProfile
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
        /// 自己修改密码
        /// </summary>
        /// <param name="updatePasswordRequestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdatePassword(UpdatePasswordRequestModel updatePasswordRequestModel)
        {
            var user= HttpContext.Items["actor"] as User;
            if (BCrypt.Net.BCrypt.Verify(updatePasswordRequestModel.OldPassword, user.Password))
            {
                var user_db = _databaseService.Users.Find(user.Id);
                user_db.Password = BCrypt.Net.BCrypt.HashPassword(updatePasswordRequestModel.NewPassword);
                //缺少对User的UpdateAt的修改
                _databaseService.SaveChanges();
            }
            else
            {
                throw new AuthenticateFailedException("The old password is not correct!");
            }
            return Ok(new UpdatePasswordResultModel(user));
        }
    }
}
