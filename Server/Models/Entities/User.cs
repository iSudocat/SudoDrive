using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Server.Libraries;
using Server.Models.VO;

namespace Server.Models.Entities
{
    [Table("users")]
    public class User : ICreateTimeStampedModel, IUpdateTimeStampedModel
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("username")]
        public string Username { get; set; }

        [Required]
        [Column("password")]
        public string Password { get; set; }

        public ICollection<GroupToUser> GroupToUser { get; set; }

        [Column("create_at")]
        public DateTime CreatedAt { get; set; }

        [Column("update_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("nickname")]
        public string Nickname { get; set; }

        [Column("status")]
        public int? Status { get; set; } = 0;

        public ICollection<UserToPermission> UserToPermission { get; set; }

        /// <summary>
        /// 判断这个用户是否有某个权限
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public bool? HasPermission(string[] permission)
        {
            bool? ret = null;

            var permissions = this.UserToPermission;
            if (permissions != null)
            {
                var hold = permissions.Select(s => s.Permission).ToList();
                ret = PermissionUtil.HasPermissionIn(hold, permission);
            }

            if (ret != null)
            {
                return ret;
            }

            var groupToUser = this.GroupToUser;
            foreach (var group in groupToUser)
            {
                var result = group.Group.HasPermission(permission);
                if (result == false) return false;
                if (result == true) ret = true;
            }

            return ret;
        }

        /// <summary>
        /// 判断这个用户是否有某个权限
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public bool? HasPermission(string permission)
        {
            return this.HasPermission(permission.Split('.'));
        }

        public UserModel ToVO()
            => new UserModel(this);
    }
}
