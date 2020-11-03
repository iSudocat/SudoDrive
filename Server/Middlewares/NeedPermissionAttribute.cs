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

            var ret = false;
            if (user == null)
            {
                var groupRepo = context.HttpContext.RequestServices.GetService(typeof(IGroupRepository)) as IGroupRepository;
                var group = groupRepo.FindByIdWithPermissions(Group.GroupID.GUEST);
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
                    var userRepo = context.HttpContext.RequestServices.GetService(typeof(IUserRepository)) as IUserRepository;
                    long userId = user.Id;
                    user = userRepo.FindByIdWithGroupsAndPermissions(user.Id);

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