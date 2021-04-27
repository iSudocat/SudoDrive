using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server.Models.DTO
{
    public class RegisterRequestModel
    {
        [Required]
        [JsonProperty("username")]
        [RegularExpression(@"^[a-zA-Z0-9-_]{4,16}$")]
        public string Username { get; set; }


        [Required]
        [JsonProperty("password")]
        [RegularExpression(@"^[^\n\r]{8,}$")]
        public string Password { get; set; }


        [JsonProperty("nickname")]
        [RegularExpression(@"^[^\n\r]{4,16}$")]
        public string Nickname { get; set; }
    }
}
