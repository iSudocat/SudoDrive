using System;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTO
{
    public class GroupListRequestModel
    {
        /**
         * 搜索关键字
         * 与关系
         */
        public string[] GroupName { get; set; }

        [Range(1, 1000)]
        public int Amount { get; set; } = 100;

        [Range(0, Int32.MaxValue)]
        public int Offset { get; set; } = 0;
    }
}
