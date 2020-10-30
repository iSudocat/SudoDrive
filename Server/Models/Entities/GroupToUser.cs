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
        [Column(Order = 1)]
        public uint GroupId { get; set; }

        [Key]
        [Column(Order =2)]
        public uint UserId { get; set; }


    }
}
