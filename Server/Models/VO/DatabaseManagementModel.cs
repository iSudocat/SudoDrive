using Newtonsoft.Json;

namespace Server.Models.VO
{
    public class DatabaseManagementModel
    {

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("ConnectionInformation")]
        public string ConnectionInfo { get; set; }
    }
}