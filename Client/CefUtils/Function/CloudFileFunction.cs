using Client.CefUtils.VO.Cloud;
using Client.Request;
using Client.Request.Response.LoginResponse;
using Client.Request.Response.UploadResponse;
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
        public static string currentPath = @"/users/sudodog/测试数据/a lot of txt";

        public DownloadTaskListVO downloadTaskListVO = new DownloadTaskListVO();
        public UploadTaskListVO uploadTaskListVO = new UploadTaskListVO();
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
        /// 获取当前路径
        /// </summary>
        /// <returns></returns>
        public string getCurrentPath()
        {
            return currentPath;
        }
        /// <summary>
        /// 直接跳转
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string goPath(string path)
        {
            currentPath = path;
            return getFileList();
        }
        /// <summary>
        /// 父目录
        /// </summary>
        /// <returns></returns>
        public string goParent()
        {
            string path = "";
            string[] paths = currentPath.Split('/');
            for (int i = 1; i < paths.Length - 1; i++)
                path += "/" + paths[i];
            currentPath = path;
            return getFileList();
        }
        /// <summary>
        /// 获取云端文件信息
        /// </summary>
        /// <returns></returns>
        public string getFileList()
        {
            if (userService == null) return null;
            FileRequest fileRequest = new FileRequest();
            fileRequest.GetFileList(currentPath,
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
        public string download(string localPath, string fileName, string id)
        {
            if (userService == null) return null;
            DownloadTaskList.Add(new FileControlBlock
            {
                FileName = fileName,
                Id = id,
                LocalPath = localPath,
                Status = StatusType.Waiting
            });
            return null;
        }
        /// <summary>
        /// 新建文件夹
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public string newFolder(string folderName)
        {
            if (userService == null) return null;
            FileRequest fileRequest = new FileRequest();
            int status = fileRequest.NewFolder(currentPath + "/" + folderName, out UploadResponse result);
            Console.WriteLine("NewFolder");
            Console.WriteLine(result);
            return result.status.ToString();
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string deleteFile(string fileName)
        {
            if (userService == null) return null;
            FileRequest fileRequest = new FileRequest();
            int status = fileRequest.Delete(out _, fileName);
            return status.ToString();
            return null;
        }
        /// <summary>
        /// 刷新并返回当前上传状态列表
        /// </summary>
        /// <returns></returns>
        public string getUploadList()
        {
            uploadTaskListVO.refresh();
            return uploadTaskListVO.GetUploadTaskListVO();
        }
        public string getDownloadList()
        {
            downloadTaskListVO.refresh();
            return downloadTaskListVO.GetDownloadTaskListVO();
        }
        public string search(string text)
        {
            FileRequest fileRequest = new FileRequest();
            fileRequest.SearchFile(text,
                out _, out List<Client.Request.Response.FileListResponse.File> fileList);
            CloudFileListVO cloudFileListVO = new CloudFileListVO(fileList);
            return JsonConvert.SerializeObject(cloudFileListVO);
        }
    }
}
