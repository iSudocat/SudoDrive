using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Server.Exceptions;
using Server.Models.DTO;
using Server.Services;

namespace Server.Middlewares
{
    public class AuthenticateFailed
    {
        private readonly RequestDelegate _next;

        public AuthenticateFailed(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IDatabaseService databaseService)
        {
            await _next(context);

            if (context.Response.StatusCode == 401 && !context.Response.HasStarted)
            {
                var e = new UnauthenticatedException(null);
                var result = new ResultModel(e.Status, e.Message, e.ApiExceptionData);
                byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result, Formatting.None));

                context.Response.ContentType = "application/json";
                await context.Response.Body.WriteAsync(buffer);
                context.Response.ContentLength = buffer.Length;
            }
        }
    }
}