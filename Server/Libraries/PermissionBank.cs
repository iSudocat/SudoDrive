namespace Server.Libraries
{
    public class PermissionBank
    {
        /// <summary>
        /// 注册权限
        /// 默认组：游客组
        /// </summary>
        public const string UserAuthRegister = "user.auth.register";

        /// <summary>
        /// 登陆权限
        /// 默认组：游客组
        /// </summary>
        public const string UserAuthLogin = "user.auth.login";

        /// <summary>
        /// 刷新登陆信息
        /// 默认组：默认用户组
        /// </summary>
        public const string UserAuthRefresh = "user.auth.refresh";


        /// <summary>
        /// 获取用户基础信息
        /// 默认组：默认用户组
        /// </summary>
        public const string UserProfileBasic = "user.profile.basic";

        /// <summary>
        /// 更新个人信息
        /// 默认组：默认用户组
        /// </summary>
        public const string UserProfileUpdateBasic = "user.profile.update.basic";

        /// <summary>
        /// 管理员更新他人信息
        /// 默认组：管理员用户组
        /// </summary>
        public const string UserProfileAdminUpdate = "user.profile.admin.update";

        /// <summary>
        /// 管理员更新他人信息
        /// 默认组：管理员用户组
        /// </summary>
        public const string UserProfileAdminList = "user.profile.admin.list";

        /// <summary>
        /// 存储系统上传文件
        /// 默认组：默认用户组
        /// </summary>
        public const string StorageFileUploadBasic = "storage.file.upload.basic";

        /// <summary>
        /// 存储系统删除文件
        /// 默认组：默认用户组
        /// </summary>
        public const string StorageFileDeleteBase = "storage.file.delete.basic";

        /// <summary>
        /// 存储系统列出/下载文件
        /// 默认组：默认用户组
        /// </summary>
        public const string StorageFileListBasic = "storage.file.list.basic";


        /// <summary>
        /// 用户组管理创建新用户组（基本权限）
        /// 默认组：默认用户组
        /// </summary>
        public const string GroupManageGroupCreateBasic = "groupmanager.group.create.basic";

        /// <summary>
        /// 用户组管理删除用户组（基本权限）
        /// 受到：groupmanager.group.operation.组名.remove 权限限制
        /// 默认组：默认用户组
        /// </summary>
        public const string GroupManageGroupDeleteBasic = "groupmanager.group.delete.basic";

        /// <summary>
        /// 用户组管理退出用户组
        /// 默认组：默认用户组
        /// </summary>
        public const string GroupManageGroupQuitBasic = "groupmanager.group.quit.basic";

        /// <summary>
        /// 用户组管理添加成员（基本权限）
        /// 受到：groupmanager.group.operation.组名.member.add 权限限制
        /// 默认组：默认用户组
        /// </summary>
        public const string GroupManageGroupMemberAddBasic = "groupmanager.group.member.add.basic";

        /// <summary>
        /// 用户组管理移除成员（基本权限）
        /// 受到：groupmanager.group.operation.组名.member.remove 权限限制
        /// 默认组：默认用户组
        /// </summary>
        public const string GroupManageGroupMemberRemoveBasic = "groupmanager.group.member.remove.basic";


        /// <summary>
        /// 生成文件访问权限（用来判断存储系统的文件结构）
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
        /// <param name="operation">操作：add / delete / remove</param>
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
