using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.VO
{
    public class UserListRequestModel
    {
        /// <summary>
        /// 与关系
        /// 精确全字匹配路径
        /// </summary>
        public string[] Username { get; set; }

        /// <summary>
        /// 或关系
        /// </summary>
        public long[] Id { get; set; }


        [Range(1, 1000)]
        public int Amount { get; set; } = 100;

        [Range(0, Int32.MaxValue)]
        public int Offset { get; set; } = 0;
    }
}