using Newtonsoft.Json;

namespace Server.Models.VO
{
    public class DatabaseManagementModel
    {

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("connectionInfo")]
        public string ConnectionInfo { get; set; }
    }
}