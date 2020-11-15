using Client.TencentCos.Task.Operation;
using COSXML;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.TencentCos.Task.List
{
    public static class UploadTaskList
    {
        private static SortedList<long, FileControlBlock> waitingList = new SortedList<long, FileControlBlock>();
        private static SortedList<long, FileControlBlock> runningList = new SortedList<long, FileControlBlock>();
        private static SortedList<long, FileControlBlock> successList = new SortedList<long, FileControlBlock>();
        private static SortedList<long, FileControlBlock> failureList = new SortedList<long, FileControlBlock>();

        private static long key = 0;

        private static int runningLimit { get; set; } = 2;

        private static Mutex waitinglistMutex = new Mutex();
        private static Mutex runninglistMutex = new Mutex();
        private static Mutex successlistMutex = new Mutex();
        private static Mutex failurelistMutex = new Mutex();

        public static void Add(FileControlBlock file)
        {
            waitinglistMutex.WaitOne();
            file.Key = key;
            waitingList.Add(key, file);
            key += 10000;
            waitinglistMutex.ReleaseMutex();
        }

        public static void Remove(long key)
        {
            runninglistMutex.WaitOne();
            runningList.Remove(key);
            runninglistMutex.ReleaseMutex();
        }

        public static void SetProgress(long key, long completed, long total)
        {
            runninglistMutex.WaitOne();
            runningList[key].Completed = completed;
            runningList[key].Total = total;
            runninglistMutex.ReleaseMutex();
        }

        public static void SetSuccess(long key)
        {
            runninglistMutex.WaitOne();
            successlistMutex.WaitOne();
            runningList[key].Status = StatusType.Success;
            successList.Add(key, runningList[key]);
            runningList.Remove(key);
            successlistMutex.ReleaseMutex();
            runninglistMutex.ReleaseMutex();
        }

        public static void SetFailure(long key, string errorMessage)
        {
            runninglistMutex.WaitOne();
            failurelistMutex.WaitOne();
            runningList[key].Status = StatusType.Failure;
            runningList[key].ErrorMessage = errorMessage;
            failureList.Add(key, runningList[key]);
            runningList.Remove(key);
            failurelistMutex.ReleaseMutex();
            runninglistMutex.ReleaseMutex();
        }

        public static StatusType GetStatus(long key)
        {
            runninglistMutex.WaitOne();
            var status = runningList[key].Status;
            runninglistMutex.ReleaseMutex();
            return status;
        }

        public static void SetStatus(long key, StatusType status)
        {
            runninglistMutex.WaitOne();
            runningList[key].Status = status;
            runninglistMutex.ReleaseMutex();
        }

        public static SortedList<long, FileControlBlock> GetWaitingList()
        {
            return waitingList;
        }

        public static SortedList<long, FileControlBlock> GetRunningList()
        {
            return runningList;
        }

        public static SortedList<long, FileControlBlock> GetSuccessList()
        {
            return successList;
        }

        public static SortedList<long, FileControlBlock> GetFailureList()
        {
            return failureList;
        }

        public static void run()
        {
            while (true)
            {
                waitinglistMutex.WaitOne();
                if (waitingList.Count != 0)
                {
                    IList<long> waitingListkeys = waitingList.Keys;
                    IList<FileControlBlock> waitingListValues = waitingList.Values;

                    int difference = runningLimit - runningList.Count;
                    for (int i = 0; i < difference; i--)
                    {
                        if (waitingList.Count > 0)
                        {
                            runninglistMutex.WaitOne();
                            runningList.Add(waitingListkeys[0], waitingListValues[0]);
                            runninglistMutex.ReleaseMutex();

                            waitingList.RemoveAt(0);
                        }
                        else break;
                    }
                }
                waitinglistMutex.ReleaseMutex();

                runninglistMutex.WaitOne();
                foreach (FileControlBlock file in runningList.Values)
                {
                    if(file.Status == StatusType.Waiting)
                    {
                        Upload upload = new Upload(file);
                        new Thread(upload.Run).Start();
                        file.Status = StatusType.Running;
                        break;
                    }
                    Thread.Sleep(1000); // 防止CosXml串线
                }
                runninglistMutex.ReleaseMutex();

                Thread.Sleep(1000);
            }
        }

    }
}
