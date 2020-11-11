using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server.Models.VO
{
    public class LoginRequestModel
    {
        [Required]
        [JsonProperty("username")]
        [RegularExpression(@"^[a-zA-Z0-9-_]{4,16}$")]
        public string Username { get; set; }


        [Required]
        [JsonProperty("password")]
        [RegularExpression(@"^[^\n\r]{8,}$")]
        public string Password { get; set; }
    }
}