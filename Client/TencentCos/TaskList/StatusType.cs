using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.TencentCos
{
    /// <summary>
    /// 任务状态
    /// </summary>
    public enum StatusType
    {
        /// <summary>
        /// 未开始
        /// </summary>
        Waiting = 0,

        /// <summary>
        /// 进行中
        /// </summary>
        Running = 1,

        /// <summary>
        /// 请求暂停
        /// </summary>
        RequestPause = 2,

        /// <summary>
        /// 已暂停
        /// </summary>
        Paused = 3,

        /// <summary>
        /// 请求开始
        /// </summary>
        RequestRusume = 4,

        /// <summary>
        /// 请求停止
        /// </summary>
        RequestCancel = 5
    }
}
