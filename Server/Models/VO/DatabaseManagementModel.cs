using Newtonsoft.Json;

namespace Server.Models.VO
{
    public class DatabaseManagementModel
    {

        [JsonProperty("Type")]
        public string Type { get; init; }

        [JsonProperty("ConnectionInformation")]
        public string ConnectionInformation { get; init; }

        [JsonProperty("ServerVersion")]
        public string ServerVersion { get; init; }
    }
}