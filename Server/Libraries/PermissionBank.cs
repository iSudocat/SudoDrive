namespace Server.Libraries
{
    public class PermissionBank
    {
        public const string UserAuthRegister = "user.auth.register";
        public const string UserAuthLogin = "user.auth.login";
        public const string UserAuthRefresh = "user.auth.refresh";
        public const string UserAuthUpdatePassword = "user.auth.updatepassword";

        public const string StorageFileUploadBasic = "storage.file.upload.basic";
        public const string StorageFileDeleteBase = "storage.file.delete.basic";
        public const string StorageFileListBasic = "storage.file.list.basic";

        public const string GroupmanageAddgroup = "groupmanage.addgroup";
        public const string GroupmanageDeletegroup = "groupmanage.deletegroup";
        public const string GroupmanageQuitgroup = "groupmanage.quitgroup";
        public const string GroupmanageDeletegroupmember = "groupmanage.deletegroupmember";
        public const string GroupmanageAddgroupmember = "groupmanage.addgroupmember";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">users / groups / root</param>
        /// <param name="name">username / groupname</param>
        /// <param name="operation">upload / delete / list</param>
        /// <returns></returns>
        public static string StoragePermission(string type, string name, string operation)
        {
            if (type == "root") return $"storage.file.root.{operation}";
            return $"storage.file.{type}.{name}.{operation}";
        }
    }
}
