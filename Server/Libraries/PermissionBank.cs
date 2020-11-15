namespace Server.Libraries
{
    public class PermissionBank
    {
        public const string UserAuthRegister = "user.auth.register";
        public const string UserAuthLogin = "user.auth.login";
        public const string UserAuthRefresh = "user.auth.refresh";

        public const string UserProfileBasic = "user.profile.basic";
        public const string UserProfileUpdateBasic = "user.profile.update.basic";
        public const string UserProfileAdminUpdate = "user.profile.admin.update";

        public const string StorageFileUploadBasic = "storage.file.upload.basic";
        public const string StorageFileDeleteBase = "storage.file.delete.basic";
        public const string StorageFileListBasic = "storage.file.list.basic";

        public const string GroupManageGroupCreateBasic = "groupmanager.group.create.basic";
        public const string GroupManageGroupDeleteBasic = "groupmanager.group.delete.basic";
        public const string GroupManageGroupQuitBasic = "groupmanager.group.quit.basic";
        public const string GroupManageGroupMemberAddBasic = "groupmanager.group.member.add.basic";
        public const string GroupManageGroupMemberDeleteBasic = "groupmanager.group.member.delete.basic";

        /// <summary>
        /// 生成文件访问权限
        /// </summary>
        /// <param name="type">权限类型：users / groups / root</param>
        /// <param name="name">权限类型对应的用户/组名：username / groupname</param>
        /// <param name="operation">操作：upload / delete / list</param>
        /// <returns></returns>
        public static string StoragePermission(string type, string name, string operation)
        {
            if (type == "root")
            {
                return $"storage.file.operation.root.{operation}";
            }
            return $"storage.file.operation.{type}.{name}.{operation}";
        }

        /// <summary>
        /// 生成组访问权限
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <param name="type">生成类型：member</param>
        /// <param name="operation">操作：add / delete</param>
        /// <returns></returns>
        public static string GroupOperationPermission(string groupName, string type, string operation)
        {
            if (type=="member")
            {
                return $"groupmanager.group.operation.{groupName}.{type}.{operation}";
            }
            return $"groupmanager.group.operation.{groupName}.{operation}";
        }
    }
}
