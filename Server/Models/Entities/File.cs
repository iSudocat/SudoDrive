using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class File : ICreateTimeStampedModel, IUpdateTimeStampedModel
    {
        /// <summary>
        /// 文件编号，自增
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 文件 MIME 类型
        /// </summary>
        [Required]
        public string Type { get; set; }

        /// <summary>
        /// 文件的存储文件夹
        /// </summary>
        [Required]
        public string Folder { get; set; }

        /// <summary>
        /// 文件的存储路径
        ///
        /// Path = Folder + Name
        /// </summary>
        [Required]
        public string Path { get; set; }

        /// <summary>
        /// 随机编号
        /// </summary>
        [Required]
        public string Guid { get; set; }

        /// <summary>
        /// 存储文件名
        /// </summary>
        [Required]
        public string StorageName { get; set; }

        /// <summary>
        /// 上传本文件的用户
        /// </summary>
        [Required]
        public User User { get; set; }
        
        /// <summary>
        /// 文件状态
        /// </summary>
        [Required]
        public FileStatus Status { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [Required]
        public long Size { get; set; }

        /// <summary>
        /// 文件哈希值
        /// </summary>
        [Required]
        public string Md5 { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public enum FileStatus
        {
            Pending = 0,
            Confirmed = 1
        }
    }
}
