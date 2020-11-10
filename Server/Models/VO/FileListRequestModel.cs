using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.VO
{
    public class FileListRequestModel
    {
        public string Folder { get; set; }

        public string PathPrefix { get; set; }

        /// <summary>
        /// 或关系
        /// 精确全字匹配路径
        /// </summary>
        public string[] Path { get; set; }

        /// <summary>
        /// 或关系
        /// </summary>
        public long[] Id { get; set; }

        /// <summary>
        /// 或关系
        /// </summary>
        public string[] Guid { get; set; }

        /// <summary>
        /// 与关系
        /// </summary>
        public string[] PathContains { get; set; }

        /// <summary>
        /// 与关系
        /// </summary>
        public string[] NameContains { get; set; }

        /// <summary>
        /// 或关系
        /// </summary>
        public string[] Type { get; set; }

        /// <summary>
        /// 是否下载
        /// 当该值为真时获取下载信息
        /// </summary>
        public bool Download { get; set; } = false;

        [Range(1, 1000)]
        public int Amount { get; set; } = 100;

        [Range(0, Int32.MaxValue)]
        public int Offset { get; set; } = 0;
    }
}