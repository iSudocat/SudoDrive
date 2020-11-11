using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Client.Request;
using COSXML;
using COSXML.CosException;
using COSXML.Model;
using COSXML.Model.Object;
using COSXML.Network;
using COSXML.Transfer;

namespace Client.TencentCos.Task.Operation
{
    public class Download
    {
        private FileControlBlock File;
        private Thread TaskStatusDetectionThread;

        private COSXMLDownloadTask downloadTask;

        public Download(FileControlBlock file)
        {
            this.File = file;
            TaskStatusDetectionThread = new Thread(TaskStatusDetection);
            TaskStatusDetectionThread.Start();
        }

        public void Run()
        {
            /*
            FileRequest fileRequest = new FileRequest();
            var res = fileRequest.Download();

            CosService cosService = new CosService();
            CosXml cosXml = cosService.getCosXml(
                res.data.token.credentials.tmpSecretId,
                res.data.token.credentials.tmpSecretKey,
                res.data.token.credentials.token,
                res.data.token.expiredTime);

            TransferConfig transferConfig = new TransferConfig();

            TransferManager transferManager = new TransferManager(cosXml, transferConfig);

            String bucket = CosConfig.Bucket; //存储桶，格式：BucketName-APPID
            String cosPath = File.RemotePath + "\\" + File.FileName; //对象在存储桶中的位置标识符，即称对象键
            string localDir = File.LocalPath;//本地文件夹
            string localFileName = File.FileName; //指定本地保存的文件名

            downloadTask = new COSXMLDownloadTask(bucket, cosPath, localDir, localFileName);

            downloadTask.progressCallback = delegate (long completed, long total)
            {
                FileTask.SetProgress(File.Key, completed, total);
                Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));                
            };
            downloadTask.successCallback = delegate (CosResult cosResult)
            {
                COSXMLDownloadTask.DownloadTaskResult result = cosResult as COSXMLDownloadTask.DownloadTaskResult;
                Console.WriteLine(result.GetResultInfo());
                string eTag = result.eTag;
                FileTask.Remove(File.Key);
            };
            downloadTask.failCallback = delegate (CosClientException clientEx, CosServerException serverEx)
            {
                if (clientEx != null)
                {
                    Console.WriteLine("CosClientException: " + clientEx);
                }
                if (serverEx != null)
                {
                    Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                }
            };
            transferManager.Download(downloadTask);
            */
        }

        public void TaskStatusDetection()
        {
            while (true)
            {
                switch (TaskList.GetStatus(File.Key))
                {
                    case StatusType.RequestPause:
                        {
                            downloadTask.Pause();
                            TaskList.SetStatus(File.Key, StatusType.Paused);
                            break;
                        }
                    case StatusType.RequestRusume:
                        {
                            downloadTask.Resume();
                            TaskList.SetStatus(File.Key, StatusType.Running);
                            break;
                        }
                    case StatusType.RequestCancel:
                        {
                            downloadTask.Cancel();
                            TaskList.Remove(File.Key);
                            break;
                        }
                    default:
                        break;
                }

                Thread.Sleep(500);
            }
        }
    }
}
