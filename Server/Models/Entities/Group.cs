using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Server.Libraries;
using Server.Models.VO;

namespace Server.Models.Entities
{
    [Table("groups")]
    public class Group : ICreateTimeStampedModel, IUpdateTimeStampedModel
    {
        public class GroupID
        {
            public static long ADMIN = 1L;
            public static long DEFAULT = 2L;
            public static long GUEST = 3;
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("group_name")]
        public string GroupName { get; set; }

        public ICollection<GroupToUser> GroupToUser { get; set; }

        [Column("create_at")]
        public DateTime CreatedAt { get; set; }

        
        [Column("update_at")]
        public DateTime UpdatedAt { get; set; }

        public ICollection<GroupToPermission> GroupToPermission { get; set; }

        /// <summary>
        /// 判断这个用户组是否有某个权限
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public bool? HasPermission(string[] permission)
        {
            // 获取本用户组拥有的权限
            var permissions = this.GroupToPermission;
            if (permissions == null) return null;
            List<string> hold = permissions.Select(groupToPermission => groupToPermission.Permission).ToList();
            hold.Sort();

            return PermissionUtil.HasPermissionIn(hold, permission);
        }

        /// <summary>
        /// 判断这个用户组是否有某个权限
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public bool? HasPermission(string permission)
        {
            return this.HasPermission(permission.Split('.'));
        }

        public GroupModel ToVO()
            => new GroupModel(this);
    }
}
