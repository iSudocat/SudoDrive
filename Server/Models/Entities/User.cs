using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Server.Models.Entities
{
    public class User : ICreateTimeStampedModel, IUpdateTimeStampedModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public IList<GroupToUser> GroupToUser { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// 判断这个用户是否有某个权限
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        public bool? HasPermission(string[] permission)
        {
            var groupIds = this.GroupToUser;
            var groups= groupIds.Select(groupToUser => groupToUser.Group);

            var ret = false;

            foreach (var group in groups)
            {
                var result = group.HasPermission(permission);
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
    }
}
