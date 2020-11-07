using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Server.Exceptions;
using Server.Models;
using Server.Models.DTO;

namespace Server.Middlewares
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            int status = 0;
            string message = "";
            object data = "";

            if (context.Exception is APIException)
            {
                var e = context.Exception as APIException;
                status = e.Status;
                data = e.ApiExceptionData ?? e.Data;
            }
            else
            {
                status = 500;
                data = context.Exception.Data;
            }

            message = context.Exception.Message;

            if (!(context.Exception is APIException) || (context.Exception is UnexpectedException)) { 
                _logger.LogError(context.Exception, message, data);
            }

            context.Result = new JsonResult(new ResultModel(status, message, data));
            context.HttpContext.Response.StatusCode = 500;

        }
    }
}