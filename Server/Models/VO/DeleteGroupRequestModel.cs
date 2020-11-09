using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.VO
{
    public class DeleteGroupRequestModel
    {
        [Required]
        [JsonProperty("groupname")]
        public string GroupName { get;private set; }
    }
}
