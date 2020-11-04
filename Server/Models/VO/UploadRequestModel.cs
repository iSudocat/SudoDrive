using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server.Models.VO
{
    public class UploadRequestModel
    {
        [Required]
        [JsonProperty("Type")]
        public string Type { get; set; }


        [Required]
        [JsonProperty("path")]
        public string Path { get; set; }

        [Required]
        [JsonProperty("size")]
        public long Size { get; set; }

        [Required]
        [JsonProperty("md5")]
        public string Md5 { get; set; }

        // TODO 文件元信息
    }
}