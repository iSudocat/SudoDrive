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

            if (databaseService == null) throw new UnexpectedException();

            if (user == null)
            {
                var group = databaseService.Groups.Find(Group.GroupID.GUEST);
                if (group == null) throw new AuthenticateFailedException();

                foreach (var s in _permission)
                {
                    var result = group.HasPermission(s);
                    if (result == false) throw new AuthenticateFailedException();
                }
                
            }
            else
            {

                foreach (var s in _permission)
                {
                    var result = user.HasPermission(s);
                    if (result == false) throw new AuthenticateFailedException();
                }
            }

            // TODO 权限判定

            // 如果出事
            return;
        }
    }
}