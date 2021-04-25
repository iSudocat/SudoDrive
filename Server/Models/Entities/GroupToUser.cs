using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    [Table("group_user")]
    public class GroupToUser
    {
        [Column("group_id")]
        public long GroupId { get; set; }

        public Group Group { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        public User User { get; set; }
    }
}
