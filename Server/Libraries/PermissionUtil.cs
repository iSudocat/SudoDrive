using System.Collections.Generic;

namespace Server.Libraries
{
    public static class PermissionUtil
    {
        public static bool? HasPermissionIn(List<string> permissionSet, string[] permission)
        {
            
            // 消极权限
            if (permissionSet.Find(s => s == "-*")?.Length > 0) return false;

            // 积极权限
            if (permissionSet.Find(s => s == "*")?.Length > 0) return true;

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
                if (permissionSet.Find(s => s == "-" + p)?.Length > 0) return false;

                // 积极权限
                if (permissionSet.Find(s => s == p)?.Length > 0) return true;

                t += ".";
            }

            t += permission[^1];

            // 消极权限
            if (permissionSet.Find(s => s == "-" + t)?.Length > 0) return false;

            // 积极权限
            if (permissionSet.Find(s => s == t)?.Length > 0) return true;

            return null;
        }
    }
}