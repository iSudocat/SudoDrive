using Microsoft.AspNetCore.Mvc;
using Server.Models.Entities;
using Server.Models.VO;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/register")]
    [ApiController]
    public class RegisterController
    {
        private IDatabaseService _databaseService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseService">通过依赖注入获得数据库对象</param>
        public RegisterController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpPost]
        public ActionResult<String> Register([FromBody] RegisterRequestModel registerRequestModel)
        {
            User user = new User();
            user.Username = registerRequestModel.Username;
            user.Password = registerRequestModel.Password;
            _databaseService.Users.Add(user);
            _databaseService.SaveChanges();
            return "Succeed";
        }

    }
}
