using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Models.DTO;
using Server.Models.VO;
using File = Server.Models.Entities.File;

namespace Server.Controllers
{
    [Route("api/file/upload")]
    [ApiController]
    public class UploadController : Controller
    {
        [HttpPost]
        public IActionResult RequestUpload([FromBody] UploadRequestModel requestModel)
        {
            // TODO 上传权限判定
            var file = new File();

            file.FilePath = requestModel.Path;
            file.FileType = requestModel.Type;
            // file.Folder = Path.GetDirectoryName(requestModel.Path) ?? "";
            // file.Name = Path.GetFileName(requestModel.Path) ?? "";
            // file.Guid = Guid.NewGuid();
            // file.Status = 等待上传
            // file.Size = requestModel.Size
            // file.Md5 = requestModel.Md5
            
            // TODO 根据 Size 和 MD5 判定是否有已上传的文件

            // 返回路径
            // var ret = new RequestUploadResultModel(file.FilePath, file.Guid, 101);

            return Ok();
        }

        



    }
}
