using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server.Models.DTO
{
    public class FileUploadRequestModel
    {
        [Required]
        [JsonProperty("type")]
        [RegularExpression(@"^[-\w.]+/[-\w.]+$")]
        public string Type { get; set; }

        [Required]
        [JsonProperty("path")]
        public string Path { get; set; }

        [Required]
        [JsonProperty("size")]
        // 最大可上传 2G （确信）
        [Range(typeof(long), "0", "5368709119")]
        public long Size { get; set; }

        [Required]
        [JsonProperty("md5")]
        [RegularExpression(@"^[0-9a-f]{32}$")]
        public string Md5 { get; set; }

        // TODO 文件元信息
    }
}