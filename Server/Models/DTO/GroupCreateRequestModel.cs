using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server.Models.DTO
{
    public class GroupCreateRequestModel
    {
        [Required]
        [JsonProperty("groupname")]
        [RegularExpression(@"^[a-zA-Z0-9-_]{4,16}$")]
        public string GroupName { get;  set; }

    }
}
