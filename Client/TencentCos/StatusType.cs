using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.TencentCos
{
    public static class StatusType
    {
        /// 任务状态 0：未开始，1：进行中，2：请求暂停，3：已暂停，4：请求开始，5：请求停止


        public const int Waiting = 0;

        public const int Running = 1;

        public const int RequestPause = 2;

        public const int Paused = 3;

        public const int RequestRusume = 4;

        public const int RequestCancel = 5;
    }
}
