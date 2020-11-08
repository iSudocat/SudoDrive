using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.CefUtils.VO
{
    public class FileInfoVO
    {
        [JsonProperty("name")]
        public string name;
        [JsonProperty("size")]
        public long size;
        [JsonProperty("lastModified")]
        public string lastModified;
        public FileInfoVO(FileInfo fileInfo)
        {
            name = fileInfo.Name;
            size = fileInfo.Length;
            lastModified = fileInfo.LastWriteTime.ToString("G");
        }
    }
}
