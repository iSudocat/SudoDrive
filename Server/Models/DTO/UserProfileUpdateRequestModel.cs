using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server.Models.DTO
{
    public class UserProfileUpdateRequestModel
    {
        [JsonProperty("nickname")]
        [RegularExpression(@"^[a-zA-Z0-9-_]{4,16}$")]
        public string Nickname { get; set; }

        [JsonProperty("oldpassword")]
        [RegularExpression(@"^[^\n\r]{8,}$")]
        public string OldPassword { get; set; }

        [JsonProperty("newpassword")]
        [RegularExpression(@"^[^\n\r]{8,}$")]
        public string NewPassword { get; set; }

        [JsonProperty("status")]
        [Range(0,1)]
        public int? Status { get; set; }

        /*用户头像
        [JsonProperty("photo")]
        public string Photo { get; set; }
        */

    }
}
