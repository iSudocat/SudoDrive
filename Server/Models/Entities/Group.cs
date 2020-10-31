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
        public uint Id { get; set; }
        [Required]
        public string GroupName { get; set; }

    }
}
