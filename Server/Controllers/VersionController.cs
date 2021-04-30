using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Server.Middlewares;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/version")]
    [NeedPermission("*")]
    public class VersionController : Controller
    {

        [HttpGet]
        public IActionResult GetVersion()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fileVersionInfo.ProductVersion;

            var ret = new Dictionary<string, string>();
            ret.Add("version", version);
            
            return Ok(ret);
        }


    }
}
