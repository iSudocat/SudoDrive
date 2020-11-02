using Microsoft.AspNetCore.Mvc;
using Server.Models.Entities;
using Server.Services;
using Server.Exceptions;

namespace Server.Controllers
{
    [Route("api/changepassword")]
    [ApiController]
    public class ChangePasswordController: Controller
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
        /// 修改密码
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult ChangePassword(string oldPassword,string newPassword)
        {
            var user= HttpContext.Items["actor"] as User;
            if (BCrypt.Net.BCrypt.Verify(oldPassword, user.Password))
            {
                var user_db = _databaseService.Users.Find(user.Id);
                user_db.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
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
