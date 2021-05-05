using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    [Table("user_permission")]
    public class UserToPermission
    {
        [Key]
        [Column("user_id")]
        public long UserId { get; set; }

        public User User { get; set; }

        [Key]
        [MaxLength(255)]
        [Column("permission")]
        public string Permission { get; set; }
    }
}