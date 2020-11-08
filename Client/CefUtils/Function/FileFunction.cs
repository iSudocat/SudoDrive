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

namespace Client.CefUtils.Function
{
    class FileFunction
    {
        public string showAllInfo()
        {
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string path = @"E:\SudoDrive\";
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            DirectoryInfo[] dics = root.GetDirectories();
            AllInfoVO allInfoVO = new AllInfoVO(files, dics);
            return allInfoVO.ToString();
        }
        public string showFileInfo(string path)
        {
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            StringBuilder results = new StringBuilder("");
            foreach (var fileInfo in files)
                results.Append(new StringBuilder(JsonConvert.SerializeObject(new FileInfoVO(fileInfo))));
            return results.ToString();
        }
        public string showFolderInfo(string path)
        {
            DirectoryInfo root = new DirectoryInfo(path);
            DirectoryInfo[] dics = root.GetDirectories();
            StringBuilder results = new StringBuilder("");
            foreach (var dic in dics)
                results.Append(new StringBuilder(JsonConvert.SerializeObject(new DirectoryInfoVO(dic))));
            return results.ToString();
        }
    }
}
