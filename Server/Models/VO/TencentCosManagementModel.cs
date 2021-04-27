using Newtonsoft.Json;

namespace Server.Models.VO
{
    public class TencentCosManagementModel
    {
        [JsonProperty("Bucket")]
        public string Bucket { get; set; }

        [JsonProperty("Region")]
        public string Region { get; set; }

        [JsonProperty("Prefix")]
        public string Prefix { get; set; }

        [JsonProperty("SecretId")]
        public string SecretId { get; set; }

        [JsonProperty("SecretKey")]
        public string SecretKey { get; set; }
    }
}