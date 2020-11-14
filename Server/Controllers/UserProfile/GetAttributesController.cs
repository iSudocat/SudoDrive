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
    [Route("api/auth/getattributes")]
    [ApiController]
    [NeedPermission(PermissionBank.UserAuthGetAttributes)]
    public class GetAttributesController:AbstractController
    {
        private IDatabaseService _databaseService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseService">通过依赖注入获得数据库对象</param>
        public GetAttributesController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// 获取用户自身的属性
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAttributes()
        {
            var user = HttpContext.Items["actor"] as User;
            var user_db = _databaseService.Users.FirstOrDefault(t => t.Username == user.Username);
            if(user_db==null)
            {
                throw new UserNotExistException("Username Does Not Exist when trying to get the user's attributes.");
            }
            return Ok(new GetAttributesResultModel(user_db));

        }
    }
}
