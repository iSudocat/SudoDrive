using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Server.Models.VO;

namespace Server.Models.Entities
{
    /// <summary>
    /// 文件实体
    ///
    /// 对于文件：
    /// Name: 文件名
    /// Type: 对应的 MIME 类型
    /// Folder: 该文件所在的文件夹
    /// Path: 该文件的目录: Folder + Name
    /// Guid: 随机生成的
    /// StorageName: 存储名
    /// User: 上传的用户
    /// Status: 文件状态
    /// Size: 文件大小
    /// Md5: 文件哈希
    /// permission: 权限
    /// 
    /// 对于文件夹：
    /// Name: 文件夹名
    /// Type: "text/directory"
    /// Folder: 上层目录名
    /// Path: 该文件夹的路径: Folder + Name
    /// Guid: [空]
    /// StorageName: [空]
    /// User: 创建的用户
    /// Status: 已确认
    /// Size: [空]
    /// Md5: [空]
    /// permission: 权限
    /// 
    /// </summary>
    [Table("files")]
    public class File : ICreateTimeStampedModel, IUpdateTimeStampedModel
    {
        /// <summary>
        /// 文件编号，自增
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [Required]
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 文件 MIME 类型
        /// </summary>
        [Required]
        [Column("type")]
        public string Type { get; set; }

        /// <summary>
        /// 文件的存储文件夹
        /// </summary>
        [Required]
        [Column("folder")]
        public string Folder { get; set; }

        /// <summary>
        /// 文件的存储路径
        ///
        /// Path = Folder + Name
        /// </summary>
        [Required]
        [Column("path")]
        public string Path { get; set; }

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

        [Required]
        [Column("user_id")]
        public long UserId { get; set; }

        /// <summary>
        /// 上传本文件的用户
        /// </summary>
        [Required]
        public User User { get; set; }
        
        /// <summary>
        /// 文件状态
        /// </summary>
        [Required]
        [Column("status")]
        public FileStatus Status { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [Column("size")]
        public long Size { get; set; }

        /// <summary>
        /// 文件哈希值
        /// </summary>
        [Column("md5")]
        public string Md5 { get; set; }
        
        /// <summary>
        /// 权限
        /// everyone
        /// users.{USERNAME}
        /// groups.{GROUPNAME}
        /// root
        /// </summary>
        [Column("permission")]
        public string Permission { get; set; }

        public string GetPermission()
        {
            if (!string.IsNullOrEmpty(Permission)) return Permission;
            if (string.IsNullOrEmpty(Path)) return "";
            if (Path == "/users") return Permission = "everyone";
            if (Path == "/groups") return Permission = "everyone";

            string type;
            string name;

            var splitsPath = Path.Split("/");

            if ((Type == "text/directory" && splitsPath.Length >= 3) || (splitsPath.Length >= 4))
            {
                // 获取上传路径的第一季第二级目录名
                type = splitsPath[1];
                name = splitsPath[2];

                switch (type)
                {
                    case "users":
                    case "groups":
                        break;
                    default:
                        type = "root";
                        break;
                }
            }
            else
            {
                // 如果是非 root 的状态
                type = "root";
                name = "";
            }

            if (type == "root")
                return Permission = "root";

            return Permission = $"{type}.{name}";
        }

        
        [Column("create_at")]
        public DateTime CreatedAt { get; set; }
        
        [Column("update_at")]
        public DateTime UpdatedAt { get; set; }

        public enum FileStatus
        {
            Pending = 0,
            Confirmed = 1
        }

        public FileModel ToVo()
            => new FileModel(this);

        public static File CreateDirectoryRecord(string name, string folder, string path, User user)
        {
            var ret = new File()
            {
                // Name: 文件夹名
                Name = name,
                // Type: "text/directory"
                Type = "text/directory",
                // Folder: 上层目录名
                Folder = folder,
                // Path: 该文件夹的路径: Folder + Name
                Path = path,
                // Guid: [空]
                Guid = null,
                // StorageName: [空]
                StorageName = "",
                // User: 创建的用户
                User = user,
                // Status: 已确认
                Status = FileStatus.Confirmed,
                // Size: [空]
                Size = 0,
                // Md5: [空]
                Md5 = null
            };
            ret.GetPermission();
            return ret;
        }
    }
}
