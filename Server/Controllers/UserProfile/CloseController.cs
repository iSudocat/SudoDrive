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
    [Route("api/auth/close")]
    [ApiController]
    [NeedPermission(PermissionBank.UserAuthClose)]
    public class CloseController: AbstractController
    {
        private IDatabaseService _databaseService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseService">通过依赖注入获得数据库对象</param>
        public CloseController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// 注销账户
        /// </summary>
        /// <param name="Close"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Close()
        {
            var user = HttpContext.Items["actor"] as User;
            var user_db = _databaseService.Users.FirstOrDefault(t => t.Username == user.Username);
            if(user_db==null)
            {
                throw new UserNotExistException("Username Does Not Exist when trying to close user.");
            }
            var grouptouser_db = _databaseService.GroupsToUsersRelation.Where(t => t.User.Username == user.Username);
           //缺少对所涉及的Group的UpdateAt的修改
            _databaseService.GroupsToUsersRelation.RemoveRange(grouptouser_db);
            _databaseService.Users.Remove(user_db);
            _databaseService.SaveChanges();
            return Ok(new CloseUserResultModel(user_db));
        }

    }
}
