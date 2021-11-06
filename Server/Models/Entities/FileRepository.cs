using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    [Table("files_repository")]
    public class FileRepository : ICreateTimeStampedModel, IUpdateTimeStampedModel
    {
        /// <summary>
        /// 文件编号，自增
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 随机编号
        /// </summary>
        [Column("guid")]
        public string Guid { get; set; }

        /// <summary>
        /// 存储文件名
        /// </summary>
        [Column("storage_name")]
        public string StorageName { get; set; }

        /// <summary>
        /// 文件哈希值
        /// </summary>
        [Column("md5")]
        public string Md5 { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [Column("size")]
        public long Size { get; set; }

        [Required] [Column("user_id")] public long UserId { get; set; }

        /// <summary>
        /// 上传本文件的用户
        /// </summary>
        [Required]
        public User User { get; set; }

        [Column("create_at")] public DateTime CreatedAt { get; set; }

        [Column("update_at")] public DateTime UpdatedAt { get; set; }
    }
}