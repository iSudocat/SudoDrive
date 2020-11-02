using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.Entities
{
    public class User : ICreateTimeStampedModel, IUpdateTimeStampedModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
