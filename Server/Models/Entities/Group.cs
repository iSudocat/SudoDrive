using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class Group : ICreateTimeStampedModel, IUpdateTimeStampedModel
    {
        public class GroupID
        {
            public static long ADMIN = 1L;
            public static long DEFAULT = 2L;
            public static long GUEST = 3;
        }

        [Key]
        public long Id { get; set; }

        [Required]
        public string GroupName { get; set; }

        public ICollection<GroupToUser> GroupToUser { get; set; }

        public DateTime CreatedAt { get; set; }

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

            // 消极权限
            if (hold.Find(s => s == "-*")?.Length > 0) return false;

            // 积极权限
            if (hold.Find(s => s == "*")?.Length > 0) return true;

            // t 表示当前检索到哪里
            // 如 permission = "a.b.c"
            // 则 t 取值两次分别为 a、a.b
            // p 取值两次分别为 a.*、a.b.*
            var t = "";
            for (var index = 0; index < permission.Length - 1; index++)
            {
                var s = permission[index];

                t += s;
                var p = t + "*";

                // 消极权限
                if (hold.Find(s => s == "-" + p)?.Length > 0) return false;

                // 积极权限
                if (hold.Find(s => s == p)?.Length > 0) return true;

                t += ".";
            }

            t += permission[^1];

            // 消极权限
            if (hold.Find(s => s == "-" + t)?.Length > 0) return false;

            // 积极权限
            if (hold.Find(s => s == t)?.Length > 0) return true;

            return null;
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
    }
}
