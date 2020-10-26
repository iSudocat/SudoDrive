using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        /// <summary>
        /// 一般方法测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return Ok();
        }

        /// <summary>
        /// 全局异常处理测试
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public IActionResult Delete()
        {
            var e = new APIException(1, "test");
            e.Data.Add("test", "value");
            throw e;
            // return BadRequest();
        }
    }
}
