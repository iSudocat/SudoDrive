using COSXML;
using COSXML.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.TencentCos
{
    public static class CosConfig
    {
        /// <summary>
        /// 存储桶地域
        /// </summary>
        public static string Region { get; set; }

        /// <summary>
        /// 存储桶，格式：BucketName-APPID
        /// </summary>
        public static string Bucket { get; set; }     
    }
}
