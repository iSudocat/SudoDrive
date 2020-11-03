using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Exceptions;
using Server.Middlewares;
using Server.Services;
using Server.Services.Implements;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [NeedPermission("test")]
    public class TestController : Controller
    {
        private IDatabaseService _databaseService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseService">通过依赖注入获得数据库对象</param>
        public TestController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// 一般方法测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        /// <summary>
        /// 全局异常处理测试
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete()
        {
            var e = new APIException(1, "test");
            e.Data.Add("test", "value");
            throw e;
            // return BadRequest();
        }
    }
}
