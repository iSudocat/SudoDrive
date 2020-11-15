using System;
using Server.Models.Entities;

namespace Server.Models.VO
{
    public class FileModel
    {
        /// <summary>
        /// 文件编号，自增
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件 MIME 类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 文件的存储文件夹
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// 文件的存储路径
        ///
        /// Path = Folder + Name
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 随机编号
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 存储文件名
        /// </summary>
        public string StorageName { get; set; }

        /// <summary>
        /// 上传本文件的用户
        /// </summary>
        public UserModel User { get; set; }

        /// <summary>
        /// 文件状态
        /// </summary>
        public File.FileStatus Status { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 文件哈希值
        /// </summary>
        public string Md5 { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public FileModel(File file)
        {
            this.Id = file.Id;
            this.Name = file.Name;
            this.Type = file.Type;
            this.Folder = file.Folder;
            this.Path = file.Path;
            this.Guid = file.Guid;
            this.StorageName = file.StorageName;
            this.User = file.User?.ToVO();
            this.Status = file.Status;
            this.Size = file.Size;
            this.Md5 = file.Md5;
            this.CreatedAt = file.CreatedAt;
            this.UpdatedAt = file.UpdatedAt;
        }
    }
}