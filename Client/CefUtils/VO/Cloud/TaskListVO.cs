using Client.TencentCos.Task;
using Client.TencentCos.Task.List;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.CefUtils.VO.Cloud
{
    public class FileControlBlockVO
    {
        public string name;
        public long completed;
        public long total;
        public FileControlBlockVO(FileControlBlock fileControlBlock)
        {
            name = fileControlBlock.FileName;
            completed = fileControlBlock.Completed;
            total = fileControlBlock.Total;
        }
    }
    public class UploadTaskListVO
    {
        public List<FileControlBlockVO> waiting = new List<FileControlBlockVO>();
        public List<FileControlBlockVO> running = new List<FileControlBlockVO>();
        public List<FileControlBlockVO> success = new List<FileControlBlockVO>();
        public List<FileControlBlockVO> fail = new List<FileControlBlockVO>();
        public void refresh()
        {
            waiting.Clear();
            SortedList<long, FileControlBlock> watingList = UploadTaskList.GetWaitingList();
            foreach(var x in watingList)
                waiting.Add(new FileControlBlockVO(x.Value));
            running.Clear();
            SortedList<long, FileControlBlock> runningList = UploadTaskList.GetRunningList();
            foreach (var x in runningList)
                running.Add(new FileControlBlockVO(x.Value));
            success.Clear();
            SortedList<long, FileControlBlock> successList = UploadTaskList.GetSuccessList();
            foreach (var x in successList)
                success.Add(new FileControlBlockVO(x.Value));
            fail.Clear();
            SortedList<long, FileControlBlock> failList = UploadTaskList.GetFailureList();
            foreach (var x in failList)
                fail.Add(new FileControlBlockVO(x.Value));
        }
        public string GetUploadTaskListVO()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class DownloadTaskListVO
    {
        public List<FileControlBlockVO> waiting = new List<FileControlBlockVO>();
        public List<FileControlBlockVO> running = new List<FileControlBlockVO>();
        public List<FileControlBlockVO> success = new List<FileControlBlockVO>();
        public List<FileControlBlockVO> fail = new List<FileControlBlockVO>();
        public void refresh()
        {
            waiting.Clear();
            SortedList<long, FileControlBlock> watingList = DownloadTaskList.GetWaitingList();
            foreach (var x in watingList)
                waiting.Add(new FileControlBlockVO(x.Value));
            running.Clear();
            SortedList<long, FileControlBlock> runningList = DownloadTaskList.GetRunningList();
            foreach (var x in runningList)
                running.Add(new FileControlBlockVO(x.Value));
            success.Clear();
            SortedList<long, FileControlBlock> successList = DownloadTaskList.GetSuccessList();
            foreach (var x in successList)
                success.Add(new FileControlBlockVO(x.Value));
            fail.Clear();
            SortedList<long, FileControlBlock> failList = DownloadTaskList.GetFailureList();
            foreach (var x in failList)
                fail.Add(new FileControlBlockVO(x.Value));
        }
        public string GetDownloadTaskListVO()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
