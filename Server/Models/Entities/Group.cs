using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class Group
    {

        [Key]
        public uint GroupId { get; set; }
        [Required]
        public string GroupName { get; set; }

    }
}
