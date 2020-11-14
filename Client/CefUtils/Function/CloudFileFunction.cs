using Client.CefUtils.VO.Cloud;
using Client.Request;
using Client.TencentCos.Task;
using Client.TencentCos.Task.List;
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
        public string upload(string localPath, string cloudPath, string fileName)
        {
            UserRequest userService = new UserRequest();
            userService.Login("sudodog", "ssss11111", out _);
            Console.WriteLine("测试fileName：" + fileName);
            UploadTaskList.Add(new FileControlBlock
            {
                FileName = fileName,
                LocalPath = localPath,
                RemotePath = cloudPath,
                Status = StatusType.Waiting
            });
            return null;
        }
        public string download(string localPath, string fileName, string guid)
        {
            UserRequest userService = new UserRequest();
            userService.Login("sudodog", "ssss11111", out _);

            DownloadTaskList.Add(new FileControlBlock
            {
                FileName = fileName,
                Guid = guid,
                LocalPath = localPath,
                Status = StatusType.Waiting
            });
            return null;
        }
    }
}
