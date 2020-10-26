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
            var ret = new ResultModel(0, "", objectResult);
            context.Result = new ObjectResult(ret);
        }
    }
}
