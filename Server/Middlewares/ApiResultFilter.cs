using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Server.Models;
using Server.Models.DTO;

namespace Server.Middlewares
{
    public class ApiResultFilter : ActionFilterAttribute
    {
   
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var objectResult = (context.Result as ObjectResult)?.Value;
            var contextItem = context.HttpContext.Items;
            var status = 0;
            try
            {
                status = (int?)contextItem["status"] ?? 0 ;
            }
            catch (KeyNotFoundException)
            {
            }

            var message = "";
            try
            {
                message = contextItem["message"] as string;
            }
            catch (KeyNotFoundException)
            {
            }

            var ret = new ResultModel(status, message, objectResult);
            context.Result = new ObjectResult(ret);
        }
    }
}
