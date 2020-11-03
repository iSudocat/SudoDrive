using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Server.Exceptions;
using Server.Services;

namespace Server.Middlewares
{
    public class AuthenticateWithHeader
    {
        private readonly RequestDelegate _next;

        public AuthenticateWithHeader(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IDatabaseService databaseService)
        {
            var user = context.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Items["actor"] = null;
            }

            long actor = 0;
            try
            {
                var claims = user.Claims;
                foreach (var claim in claims)
                {
                    if (claim.Type == ClaimTypes.Actor)
                    {
                        actor = long.Parse(claim.Value);
                    }
                }
            }
            catch (Exception)
            {
                throw new InvalidArgumentException();
            }

            // 获取到当前的登录用户
            var userEntity = databaseService.Users.Find(actor);

            // 如果找不到， userEntity 会为 null
            context.Items["actor"] = userEntity;

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}