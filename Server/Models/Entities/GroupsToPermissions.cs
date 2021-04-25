using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    [Table("group_permission")]
    public class GroupToPermission
    {
        [Key]
        [Column("group_id")]
        public long GroupId { get; set; }

        public Group Group { get; set; }

        [Key]
        [MaxLength(255)]
        [Column("permission")]
        public string Permission { get; set; }
    }
}
