using Client.CefUtils.VO.Cloud;
using Client.Request;
using Client.Request.Response.LoginResponse;
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
        public static UserRequest userService = null;
        public static LoginResponse loginResponse;
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string login(string userName, string password)
        {
            userService = new UserRequest();
            userService.Login(userName, password, out loginResponse);
            return JsonConvert.SerializeObject(loginResponse);
        }
        /// <summary>
        ///  获取登录信息
        /// </summary>
        /// <returns></returns>
        public string getLoginInfo()
        {
            return JsonConvert.SerializeObject(loginResponse);
        }
        /// <summary>
        /// 获取云端文件信息
        /// </summary>
        /// <returns></returns>
        public string getFileList()
        {
            if (userService == null) return null;
            FileRequest fileRequest = new FileRequest();
            fileRequest.GetFileList("/users/sudodog/测试数据/a lot of txt",
                out int status, out List<Client.Request.Response.FileListResponse.File> fileList);
            CloudFileListVO cloudFileListVO = new CloudFileListVO(fileList);
            return JsonConvert.SerializeObject(cloudFileListVO);
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="localPath"></param>
        /// <param name="cloudPath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string upload(string localPath, string cloudPath, string fileName)
        {
            if (userService == null) return null;
            UploadTaskList.Add(new FileControlBlock
            {
                FileName = fileName,
                LocalPath = localPath,
                RemotePath = cloudPath,
                Status = StatusType.Waiting
            });
            return null;
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="localPath"></param>
        /// <param name="fileName"></param>
        /// <param name="guid"></param>
        /// <returns></returns>
        public string download(string localPath, string fileName, string guid)
        {
            if (userService == null) return null;
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
