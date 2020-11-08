using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.CefUtils.VO
{
    public class DirectoryInfoVO
    {
        [JsonProperty("name")]
        public string name;
        [JsonProperty("lastModified")]
        public DateTime lastModified;
        public DirectoryInfoVO(DirectoryInfo directoryInfo)
        {
            name = directoryInfo.Name;
            lastModified = directoryInfo.LastWriteTime;
        }
    }
}
