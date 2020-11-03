using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class GroupToUser
    {
        [Key]
        public long GroupId { get; set; }

        public Group Group { get; set; }

        [Key]
        public long UserId { get; set; }

        public User User { get; set; }
    }
}
