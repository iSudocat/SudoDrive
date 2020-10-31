using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class File
    {
        [Key]
        public uint Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FileType { get; set; }

        [Required]
        public string FilePath{ get; set; }

        [Required]
        public DateTime LastUploadTime { get; set; }

        [Required]
        public DateTime LastUpdateTime { get; set; }

        [Required]
        public DateTime LastAccessTime { get; set; }

        /// <summary>
        /// 最后上传用户的UserId
        /// </summary>
        [Required]
        public uint UserId { get; set; }
        [ForeignKey("GroupId")]
        public User User { get; set; }

        [Required]
        public string PermissionName { get; set; }



    }
}