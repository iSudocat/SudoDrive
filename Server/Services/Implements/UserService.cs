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

        public bool IsValid(LoginRequestModel req, out User loginUser)
        {
            loginUser = _databaseService.Users.Where(s => s.Username == req.Username).FirstOrDefault(null);

            if (loginUser == null)
            {
                return false;
            }

            if (BCrypt.Net.BCrypt.Verify(req.Password, loginUser.Password))
            {
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
