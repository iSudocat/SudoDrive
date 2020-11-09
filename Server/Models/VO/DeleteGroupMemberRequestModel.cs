using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.VO
{
    public class DeleteGroupMemberRequestModel
    {
        [Required]
        [JsonProperty("groupname")]
        public string GroupName { get;private set; }

        [Required]
        [JsonProperty("username")]
        public string UserName { get;private set; }
    }
}
