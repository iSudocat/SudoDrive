using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.VO
{
    public class AddGroupMemberRequestModel
    {
        [Required]
        [JsonProperty("groupname")]
        [RegularExpression(@"^[a-zA-Z0-9-_]{4,16}$")]
        public string GroupName { get; private set; }

        [Required]
        [JsonProperty("username")]
        [RegularExpression(@"^[a-zA-Z0-9-_]{4,16}$")]
        public string UserName { get;private set; }
    }
}
