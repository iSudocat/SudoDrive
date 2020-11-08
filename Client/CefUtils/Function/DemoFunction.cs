using CefSharp.Web;
using Client.CefUtils.VO;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client.CefUtils.Function
{
    public class DemoFunction
    {
        private FolderBrowserDialog folderBrowserDialog1;
        public string selectFolder()
        {
            string folderName = "Empty Folder";
            Thread thread = new Thread(() =>
            {
                folderBrowserDialog1 = new FolderBrowserDialog();
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    folderName = folderBrowserDialog1.SelectedPath;
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return folderName;
        }
        public string showFileInfo()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            StringBuilder results = new StringBuilder("");
            foreach (var fileInfo in files)
                results.Append(new StringBuilder(JsonConvert.SerializeObject(new FileInfoVO(fileInfo))));
            return results.ToString();
        }
        public string showFolderInfo()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            DirectoryInfo root = new DirectoryInfo(path);
            DirectoryInfo[] dics = root.GetDirectories();
            return JsonConvert.SerializeObject(dics);
        }

        public double Add(int a, int b)
        {
            return a + b;
        }
    }
}