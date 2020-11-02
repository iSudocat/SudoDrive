using Microsoft.AspNetCore.Mvc;
using Server.Models.Entities;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        
        //PUT: api/changepassword
        [HttpPut]
        public ActionResult changePassword(string oldPassword,string newPassword)
        {
            var user= HttpContext.Items["actor"] as User;
            if (BCrypt.Net.BCrypt.Verify(oldPassword, user.Password))
            {
                user.Password = newPassword;
            }
            else
            {
                return BadRequest("the oldpassword is not correct!");
            }
            return NoContent();
        }
    }
}
