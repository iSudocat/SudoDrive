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
    [Route("api/auth/{username}/delete")]
    [ApiController]
    [NeedPermission(PermissionBank.UserAuthDelete)]
    public class DeleteUserController:AbstractController
    {
        private IDatabaseService _databaseService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseService">通过依赖注入获得数据库对象</param>
        public DeleteUserController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpDelete]
        public IActionResult DeleteUser(string username)
        {
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9-_]{4,16}$"))
            {
                throw new UsernameInvalidException("The username you enter is invalid when trying to delete it.");
            }
            string permission = PermissionBank.UserOperationPermission(username, "", "delete");
            var user_actor = HttpContext.Items["actor"] as User;
            if (!(bool)user_actor.HasPermission(permission))
            {
                throw new AuthenticateFailedException("not has enough permission when trying to delete a user.");
            }
            var user_db = _databaseService.Users.FirstOrDefault(t => t.Username == username);
            if (user_db == null)
            {
                throw new UserNotExistException("Username Does Not Exist when trying to delete user.");
            }
            var grouptouser_db = _databaseService.GroupsToUsersRelation.Where(t => t.User.Username == user_db.Username);
           //缺少对所涉及Group的UpdateAt的修改
            _databaseService.GroupsToUsersRelation.RemoveRange(grouptouser_db);
            _databaseService.Users.Remove(user_db);
            _databaseService.SaveChanges();
            return Ok(new DeleteUserResultModel(user_db));
        }
    }   
}
