using Newtonsoft.Json;

namespace Server.Models.VO
{
    public class TencentCosManagementModel
    {
        [JsonProperty("Bucket")]
        public string Bucket { get; init; }

        [JsonProperty("Region")]
        public string Region { get; init; }

        [JsonProperty("Prefix")]
        public string Prefix { get; init; }

        [JsonProperty("SecretId")]
        public string SecretId { get; init; }

        [JsonProperty("SecretKey")]
        public string SecretKey { get; init; }
    }
}