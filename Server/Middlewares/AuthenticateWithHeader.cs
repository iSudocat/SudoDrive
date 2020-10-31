using System;
using System.IdentityModel.Tokens.Jwt;
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

        private IDatabaseService _databaseService;

        public AuthenticateWithHeader(RequestDelegate next, IDatabaseService databaseService)
        {
            _next = next;
            _databaseService = databaseService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string auth = context.Request.Headers["Authorization"];

            if (auth != null)
            {
                var auths = auth.Split(' ');

                if (auths.Length != 2)
                {
                    throw new InvalidArgumentException();
                }

                auth = auths[1];

                UInt64 actor = 0;

                try
                {
                    JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                    var parsedToken = jwtSecurityTokenHandler.ReadJwtToken(auth);

                    var claims = parsedToken.Claims;

                    foreach (var claim in claims)
                    {
                        if (claim.Type == ClaimTypes.Actor)
                        {
                            actor = UInt64.Parse(claim.Value);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new InvalidArgumentException();
                }

                // 获取到当前的登录用户
                context.Items["actor"] = _databaseService.Users.Find(actor);

            }

            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}