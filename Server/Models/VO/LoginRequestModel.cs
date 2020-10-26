using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server.Models.VO
{
    public class LoginRequestModel
    {
        [Required]
        [JsonProperty("username")]
        public string Username { get; set; }


        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}