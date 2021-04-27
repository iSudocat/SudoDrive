using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server.Models.DTO
{
    public class FileUploadConfirmRequestModel
    {
        [Required]
        [JsonProperty("id")]
        [Range(1, Int32.MaxValue)]
        public long Id { get; set; }

        [JsonProperty("guid")]
        [RegularExpression(@"^[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}$")]
        public string Guid { get; set; }
    }
}