using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Server.Exceptions;
using Server.Models.Entities;
using Server.Services;

namespace Server.Middlewares
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method
        ,AllowMultiple = true, Inherited = true)]
    public class NeedPermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private string[] _permission;
        public NeedPermissionAttribute(params string[] permission)
        {
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["actor"] as User;
            var databaseService = context.HttpContext.RequestServices.GetService(typeof(IDatabaseService)) as IDatabaseService;

            List<Group> groups = new List<Group>();

            if (user == null)
            {
                groups.Add(databaseService.Groups.Find(3));
            }
            else
            {
                var groupIds = user.GroupToUser;
                groups.AddRange(groupIds.Select(groupToUser => groupToUser.Group));
            }

            // TODO 权限判定

            // 如果出事
            return;
        }
    }
}