using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.VO
{
    public class UpdateProfileRequestModel
    {
        [Required]
        [JsonProperty("username")]
        public string Username { get; set; }      

        
        [JsonProperty("newnickname")]
        [RegularExpression(@"^[a-zA-Z0-9-_]{4,16}$")]
        public string NewNickname { get; set; }

        [JsonProperty("oldpassword")]
        public string OldPassword { get; set; }

        [JsonProperty("newpassword")]
        [RegularExpression(@"^[^\n\r]{8,}$")]
        public string NewPassword { get; set; }

    }
}
