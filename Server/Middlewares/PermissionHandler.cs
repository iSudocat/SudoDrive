using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Server.Exceptions;
using Server.Models.Entities;

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

            if (user == null)
            {
                throw new UnauthenticatedException();
            }

            // TODO 权限判定

            // 如果出事
            return;
        }
    }
}