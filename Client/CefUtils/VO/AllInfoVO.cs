using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Client.CefUtils.VO
{
    class AllInfoVO
    {
        [JsonProperty("file")]
        public FileInfoVO[] files;
        [JsonProperty("directory")]
        public DirectoryInfoVO[] directories;
        public AllInfoVO(FileInfo[] fileInfo, DirectoryInfo[] directoryInfo)
        {
            files = new FileInfoVO[fileInfo.Length];
            directories = new DirectoryInfoVO[directoryInfo.Length];
            for(int i=0;i<fileInfo.Length;i++)
                files[i] = new FileInfoVO(fileInfo[i]);
            for (int i = 0; i < directoryInfo.Length; i++)
                directories[i] = new DirectoryInfoVO(directoryInfo[i]);
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
