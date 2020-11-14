using Client.CefUtils.VO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Request;
using Client.Request.Response.UploadResponse;
using Client.Request.Response.FileListResponse;
using Client.Request.Response;
using Client.CefUtils.VO.Cloud;

namespace Client.CefUtils.Function
{
    public class LocalFileFunction
    {
        /// <summary>
        /// 维护一个当前查看的路径
        /// </summary>
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        /// <summary>
        /// 返回父目录，由前端阻止在盘符根目录下的使用
        /// </summary>
        /// <returns>父目录信息</returns>
        public string toParent()
        {
            DirectoryInfo root = new DirectoryInfo(path);
            path = root.Parent.FullName;
            return showAllInfo();
        }
        /// <summary>
        /// 进入子目录
        /// </summary>
        /// <param name="childName"></param>
        /// <returns>子目录信息</returns>
        public string toChild(string childName)
        {
            path += @"\" + childName;
            return showAllInfo();
        }
        /// <summary>
        /// 返回当前路径下的所有文件夹以及文件的信息
        /// </summary>
        /// <returns></returns>
        public string showAllInfo()
        {
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            DirectoryInfo[] dics = root.GetDirectories();
            AllInfoVO allInfoVO = new AllInfoVO(path, files, dics);
            return allInfoVO.ToString();
        }
        /// <summary>
        /// 返回当前路径下所有文件信息
        /// </summary>
        /// <returns></returns>
        public string showFileInfo()
        {
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            StringBuilder results = new StringBuilder("");
            foreach (var fileInfo in files)
                results.Append(new StringBuilder(JsonConvert.SerializeObject(new FileInfoVO(fileInfo))));
            return results.ToString();
        }
        /// <summary>
        /// 返回当前路径下所有文件夹信息
        /// </summary>
        /// <returns></returns>
        public string showFolderInfo()
        {
            DirectoryInfo root = new DirectoryInfo(path);
            DirectoryInfo[] dics = root.GetDirectories();
            StringBuilder results = new StringBuilder("");
            foreach (var dic in dics)
                results.Append(new StringBuilder(JsonConvert.SerializeObject(new DirectoryInfoVO(dic))));
            return results.ToString();
        }
        /// <summary>
        /// 返回用户系统上所有盘符路径
        /// </summary>
        /// <returns></returns>
        public string showAllDrives()
        {
            String[] drives = Environment.GetLogicalDrives();
            return JsonConvert.SerializeObject(drives);
        }
        /// <summary>
        /// 返回目标盘符的文件信息
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public string switchDriver(string driver)
        {
            path = driver;
            return showAllInfo();
        }
    }
}
