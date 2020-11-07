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
        public string Password { get; set; }
    }
}