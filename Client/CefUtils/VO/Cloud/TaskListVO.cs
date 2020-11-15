using Client.TencentCos.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.CefUtils.VO.Cloud
{
    public class FileControlBlockVO
    {
        public string fileName;
        public long completed;
        public long total;
        public FileControlBlockVO(FileControlBlock fileControlBlock)
        {
            fileName = fileControlBlock.FileName;
            completed = fileControlBlock.Completed;
            total = fileControlBlock.Total;
        }
    }
    public class UploadTaskListVO
    {
        void refresh()
        {

        }
    }
    public class DownloadTaskListVO
    {
        void refresh()
        {

        }
    }
}
