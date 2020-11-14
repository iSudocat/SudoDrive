using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.VO
{
    public class ChangeUsernameRequestModel
    {
        [Required]
        [JsonProperty("oldusername")]
        public string OldUsername { get; set; }


        [Required]
        [JsonProperty("newusername")]
        [RegularExpression(@"^[a-zA-Z0-9-_]{4,16}$")]
        public string NewUsername { get; set; }
    }
}
