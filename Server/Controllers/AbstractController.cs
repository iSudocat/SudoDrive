using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public abstract class AbstractController : Controller
    {
        public void SetApiResultStatus(int? status, string? message = null)
        {
            if (status != null)
                this.HttpContext.Items["status"] = status;
            if (message != null)
                this.HttpContext.Items["message"] = message;
        }
    }
}