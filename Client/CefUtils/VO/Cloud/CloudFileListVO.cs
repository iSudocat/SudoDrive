using Client.Request.Response.FileListResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.CefUtils.VO.Cloud
{
    public class CloudFile
    {
        public long id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string folder { get; set; }
        public string path { get; set; }
        public string guid { get; set; }
        public string storageName { get; set; }
        public User user { get; set; }
        public int status { get; set; }
        public long size { get; set; }
        public string md5 { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public CloudFile(Client.Request.Response.FileListResponse.File file)
        {
            id = file.id;
            name = file.name;
            type = file.type;
            folder = file.folder;
            path = file.path;
            guid = file.guid;
            storageName = file.storageName;
            user = file.user;
            status = file.status;
            size = file.size;
            md5 = file.md5;
            createdAt = file.createdAt.ToString("G");
            updatedAt = file.updatedAt.ToString("G");
        }
    }
    public class CloudFileListVO
    {
        public List<CloudFile> cloudFileList;
        public List<CloudFile> cloudDirectoryList;
        public CloudFileListVO(List<Client.Request.Response.FileListResponse.File> fileList)
        {
            cloudFileList = new List<CloudFile>();
            foreach(var x in fileList)
            {
                CloudFile y = new CloudFile(x);
                cloudFileList.Add(y);
            }
        }
    }
}
