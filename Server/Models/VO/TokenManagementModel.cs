using Newtonsoft.Json;

namespace Server.Models.VO
{
    public class TokenManagementModel
    {
        [JsonProperty("Secret")]
        public string Secret { get; init; }

        [JsonProperty("Issuer")]
        public string Issuer { get; init; }

        [JsonProperty("Audience")]
        public string Audience { get; init; }

        [JsonProperty("AccessExpiration")]
        public int AccessExpiration { get; init; }

        [JsonProperty("RefreshExpiration")]
        public int RefreshExpiration { get; init; }
    }
}