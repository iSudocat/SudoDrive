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

        private static SortedList<long, File> waitingList = new SortedList<long, File>();
        private static SortedList<long, File> runningList = new SortedList<long, File>();

        private static long key = 0;

        private static int runningLimit { get; set; } = 3;

        private static Mutex waitinglistMutex = new Mutex();
        private static Mutex runninglistMutex = new Mutex();

        public static void Add(File file)
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

        public static int GetStatus(long key)
        {
            runninglistMutex.WaitOne();
            int status = runningList[key].Status;
            runninglistMutex.ReleaseMutex();
            return status;
        }

        public static void SetStatus(long key, int status)
        {
            runninglistMutex.WaitOne();
            runningList[key].Status = status;
            runninglistMutex.ReleaseMutex();
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
                    IList<File> waitingListValues = waitingList.Values;

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

                runninglistMutex.WaitOne();
                foreach (File file in runningList.Values)
                {
                    switch (file.Status)
                    {
                        case StatusType.Waiting:
                            {
                                //TODO 从后端获取密钥
                                string tmpSecretId = "";
                                string tmpSecretKey = "";
                                string tmpToken = "";
                                long tmpExpireTime = 0;

                                CosService cosService = new CosService();
                                CosXml cosXml = cosService.getCosXml(tmpSecretId, tmpSecretKey, tmpToken, tmpExpireTime);
                                FileService fileService = new FileService(file, cosXml);

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
