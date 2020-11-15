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
    [Route("api/user/{username}")]
    [ApiController]
    [NeedPermission(PermissionBank.UserAuthDelete)]
    public class DeleteUserController : AbstractController
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

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteUser(string username)
        {
            //判断用户名合法
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9-_]{4,16}$"))
            {
                throw new UsernameInvalidException("The username you enter is invalid when trying to delete it.");
            }
            //判断该用户存在，并获取数据库中的该用户
            var user_db = _databaseService.Users.FirstOrDefault(t => t.Username == username);
            if (user_db == null)
            {
                throw new UserNotExistException("Username Does Not Exist when trying to delete it.");
            }
            //判断权限
            string permission = PermissionBank.UserOperationPermission(username, "", "delete");
            var user_actor = HttpContext.Items["actor"] as User;
            if (user_actor.HasPermission(permission) != true)
            {
                throw new AuthenticateFailedException("not has enough permission when trying to delete a user.");
            }
            //存储被删除的用户的信息，用于返回结果
            long DeletedUserId = user_db.Id;
            string DeletedUserUsername = user_db.Username;
            //执行删除
            var grouptouser_db = _databaseService.GroupsToUsersRelation.Where(t => t.UserId == user_db.Id);
            _databaseService.GroupsToUsersRelation.RemoveRange(grouptouser_db);
            _databaseService.Users.Remove(user_db);
            _databaseService.SaveChanges();
            //返回结果
            return Ok(new DeleteUserResultModel(DeletedUserId, DeletedUserUsername));
        }
    }
}
