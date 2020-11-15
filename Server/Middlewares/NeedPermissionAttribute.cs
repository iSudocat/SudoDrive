using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Server.Exceptions;
using Server.Models.DTO;
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

        private void SetAuthenticateFailedException(AuthorizationFilterContext context, object data = null)
        {
            var e = new AuthenticateFailedException(data);
            context.Result = new JsonResult(new ResultModel(e.Status, e.Message, e.ApiExceptionData));
            context.HttpContext.Response.StatusCode = 403;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["actor"] as User;
            var databaseService = context.HttpContext.RequestServices.GetService(typeof(IDatabaseService)) as IDatabaseService;

            if (databaseService == null) throw new UnexpectedException();

            var ret = false;
            if (user == null)
            {
                var group = databaseService.Groups
                    .Include( s => s.GroupToPermission)
                    .FirstOrDefault(s => s.Id == Group.GroupID.GUEST);
                if (group == null)
                {
                    this.SetAuthenticateFailedException(context);
                    return;
                }
                
                foreach (var s in _permission)
                {
                    var result = group.HasPermission(s);
                    if (result == false)
                    {
                        this.SetAuthenticateFailedException(context);
                        return;
                    }
                    if (result == true) ret = true;
                }
            }
            else
            {
                foreach (var s in _permission)
                {
                    var result = user.HasPermission(s);
                    if (result == false)
                    {
                        this.SetAuthenticateFailedException(context);
                        return;
                    }
                    if (result == true) ret = true;
                }
            }

            if (!ret)
            {
                this.SetAuthenticateFailedException(context);
                return;
            }

            return;
        }
    }
}