using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.VO
{
    public class ChangePasswordRequestModel
    {
        [Required]
        [JsonProperty("oldpassword")]
        public string OldPassword { get; set; }


        [Required]
        [JsonProperty("newpassword")]
        [RegularExpression(@"^[^\n\r]{8,}$")]
        public string NewPassword { get; set; }
    }
}
