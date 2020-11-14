using Client.CefUtils.VO.Cloud;
using Client.Request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.CefUtils.Function
{
    public class CloudFileFunction
    {
        public string test()
        {
            return "Test";
        }
        /// <summary>
        /// 获取云端文件信息
        /// </summary>
        /// <returns></returns>
        public string getFileList()
        {
            UserRequest userService = new UserRequest();
            userService.Login("sudodog", "ssss11111", out _);
            FileRequest fileRequest = new FileRequest();
            fileRequest.GetFileList("/users/sudodog/测试数据/a lot of txt",
                out int status, out List<Client.Request.Response.FileListResponse.File> fileList);
            CloudFileListVO cloudFileListVO = new CloudFileListVO(fileList);
            return JsonConvert.SerializeObject(cloudFileListVO);
        }
    }
}
