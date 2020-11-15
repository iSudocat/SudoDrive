using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.TencentCos.Task
{
    /// <summary>
    /// 任务队列的文件控制块
    /// </summary>
    public class FileControlBlock
    {
        /// <summary>
        /// 列表内部处理Key
        /// </summary>
        public long Key { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 本地路径（不含文件名，路径最后无“\”）
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// 远端路径（不含文件名，路径最后无“\”）
        /// </summary>
        public string RemotePath { get; set; }

        /// <summary>
        /// 已上传文件大小
        /// </summary>
        public long Completed { get; set; }

        /// <summary>
        /// 总文件大小
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 任务状态 0：未开始，1：进行中，2：请求暂停，3：已暂停，4：请求开始，5：请求停止，6：成功，7：失败
        /// </summary>
        public StatusType Status { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }

}
