using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Request.Response.FileListResponse
{
    public class FileListResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public List<File> files { get; set; }
        public int amount { get; set; }
        public int offset { get; set; }
        public TencentCos tencentCos { get; set; }
        public Token token { get; set; }
    }

    public class File
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
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    public class User
    {
        public long id { get; set; }
        public string username { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }

    public class TencentCos
    {
        public string region { get; set; }
        public string bucket { get; set; }
    }

    public class Token
    {
        public Credentials credentials { get; set; }
        public long expiredTime { get; set; }
        public string expiration { get; set; }
        public string requestId { get; set; }
        public long startTime { get; set; }
    }

    public class Credentials
    {
        public string token { get; set; }
        public string tmpSecretId { get; set; }
        public string tmpSecretKey { get; set; }
    }
}
