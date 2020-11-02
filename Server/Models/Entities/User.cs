using System.ComponentModel.DataAnnotations;

namespace Server.Models.Entities
{
    public class User
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
