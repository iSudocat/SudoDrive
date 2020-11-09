using COSXML;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client.TencentCos
{
    public static class FileTask
    {

        private static SortedList<long, FCB> waitingList = new SortedList<long, FCB>();
        private static SortedList<long, FCB> runningList = new SortedList<long, FCB>();
        private static SortedList<long, FCB> successList = new SortedList<long, FCB>();
        private static SortedList<long, FCB> failureList = new SortedList<long, FCB>();

        private static long key = 0;

        private static int runningLimit { get; set; } = 3;

        private static Mutex waitinglistMutex = new Mutex();
        private static Mutex runninglistMutex = new Mutex();
        private static Mutex successlistMutex = new Mutex();
        private static Mutex failurelistMutex = new Mutex();

        public static void Add(FCB file)
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

        public static void SetFailure(long key)
        {
            runninglistMutex.WaitOne();
            failurelistMutex.WaitOne();
            runningList[key].Status = StatusType.Failure;
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

        public static SortedList<long, FCB> GetWaitingList()
        {
            return waitingList;
        }

        public static SortedList<long, FCB> GetRunningList()
        {
            return runningList;
        }

        public static SortedList<long, FCB> GetSuccessList()
        {
            return successList;
        }

        public static SortedList<long, FCB> GetFailureList()
        {
            return failureList;
        }


        public static void run()
        {
            while (true)
            {
                if (waitingList.Count == 0 && runningList.Count == 0)
                {
                    key = 0;    //复位key
                }

                if (waitingList.Count != 0)
                {
                    IList<long> waitingListkeys = waitingList.Keys;
                    IList<FCB> waitingListValues = waitingList.Values;

                    int difference = runningLimit - runningList.Count;
                    for (int i = 0; i < difference; i--)
                    {
                        if (waitingList.Count > 0)
                        {
                            runninglistMutex.WaitOne();
                            runningList.Add(waitingListkeys[0], waitingListValues[0]);
                            runninglistMutex.ReleaseMutex();

                            waitinglistMutex.WaitOne();
                            waitingList.RemoveAt(0);
                            waitinglistMutex.ReleaseMutex();
                        }
                        else break;
                    }
                }

                //Console.WriteLine("FileTask is running...");

                runninglistMutex.WaitOne();
                foreach (FCB file in runningList.Values)
                {
                    switch (file.Status)
                    {
                        case StatusType.Waiting:
                            {
                                FileService fileService = new FileService(file);

                                switch (file.Operation)
                                {
                                    case 0:
                                        new Thread(fileService.Test).Start();
                                        file.Status = StatusType.Running;
                                        break;
                                    case OperationType.Upload:
                                        new Thread(fileService.Upload).Start();
                                        file.Status = StatusType.Running;
                                        break;
                                    case OperationType.Download:
                                        new Thread(fileService.Download).Start();
                                        file.Status = StatusType.Running;
                                        break;
                                    case OperationType.Delete:
                                        new Thread(fileService.Delete).Start();
                                        file.Status = StatusType.Running;
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            }
                        default:
                            break;
                    }
                }
                runninglistMutex.ReleaseMutex();

                Thread.Sleep(500);
            }
        }


        
    }
}
