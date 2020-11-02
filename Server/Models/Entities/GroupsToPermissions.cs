using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class GroupToPermission
    {
        [Key]
        public long GroupId { get; set; }

        [Key]
        [MaxLength(255)]
        public string Permission { get; set; }
    }
}
