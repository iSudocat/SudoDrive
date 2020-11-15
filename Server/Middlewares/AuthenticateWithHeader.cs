using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Server.Exceptions;
using Server.Models.DTO;
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
            var userEntity = databaseService.Users
                .Include(s => s.GroupToUser)
                .ThenInclude(s => s.Group)
                .ThenInclude(s => s.GroupToPermission)
                .FirstOrDefault(s => s.Id == actor);

            if (userEntity != null && userEntity.Status != 0)
            {
                var e = new BannedFromServerException();
                var result = new ResultModel(e.Status, e.Message, e.ApiExceptionData);
                byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result, Formatting.None));

                context.Response.StatusCode = 403;
                context.Response.ContentType = "application/json";
                await context.Response.Body.WriteAsync(buffer);
                context.Response.ContentLength = buffer.Length;
            }
            else
            {       
                // 如果找不到， userEntity 会为 null
                context.Items["actor"] = userEntity;

                // Call the next delegate/middleware in the pipeline
                await _next(context);
            }
        }
    }
}