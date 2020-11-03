using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.Entities;
using Server.Models.VO;

namespace Server.Services.Implements
{
    public class UserService : IUserService
    {
        private IDatabaseService _databaseService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseService">通过依赖注入获得数据库对象</param>
        public UserService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// 校验用户密码
        /// </summary>
        /// <param name="req"></param>
        /// <param name="loginUser"></param>
        /// <returns></returns>
        public bool IsValid(LoginRequestModel req, out User loginUser)
        {
            // 从数据库中获取用户信息
            loginUser = _databaseService.Users.FirstOrDefault(s => s.Username == req.Username);

            // 若该用户不存在
            if (loginUser == null)
            {
                return false;
            }

            // 检查密码是否正确
            if (BCrypt.Net.BCrypt.Verify(req.Password, loginUser.Password))
            {
                // 登录成功，更新密码状态并保存
                loginUser.Password = BCrypt.Net.BCrypt.HashPassword(req.Password);
                _databaseService.SaveChanges();
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
