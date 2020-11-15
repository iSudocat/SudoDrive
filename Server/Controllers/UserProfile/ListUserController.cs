using Microsoft.AspNetCore.Mvc;
using Server.Libraries;
using Server.Middlewares;
using Server.Models.VO;
using Server.Services;
using Server.Models.DTO;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace Server.Controllers.UserProfile
{
    [Route("api/user/")]
    [ApiController]
    [NeedPermission(PermissionBank.UserProfileAdminList)]
    public class ListUserController : AbstractController
    {
        private IDatabaseService _databaseService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="databaseService">通过依赖注入获得数据库对象</param>
        public ListUserController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// 列出所有用户
        /// </summary>
        /// <param name="updateProfileRequestModel"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UpdateProfile([FromQuery] UserListRequestModel requestModel)
        {
            var result = _databaseService.Users
                .Include(s => s.GroupToUser)
                .ThenInclude(s => s.Group).AsSingleQuery();

            // 按用户名关键字匹配
            if (requestModel.Username?.Length > 0)
            {
                foreach (var c in requestModel.Username)
                {
                    result = result.Where(s => s.Username.Contains(c));
                }
            }

            // 按照 ID 筛选
            if (requestModel.Id?.Length >= 0)
            {
                result = result.Where(s => requestModel.Id.Contains(s.Id));
            }

            // 添加其他的搜索条件

            result = result.Skip(requestModel.Offset);
            result = result.Take(requestModel.Amount);

            return Ok(new UserListResultModel(result, requestModel.Amount, requestModel.Offset));
        }
    }
}
