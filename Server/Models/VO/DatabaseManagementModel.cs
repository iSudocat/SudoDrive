using Newtonsoft.Json;

namespace Server.Models.VO
{
    public class DatabaseManagementModel
    {
        [JsonProperty("connectionInfo")]
        public string ConnectionInfo { get; set; }
    }
}