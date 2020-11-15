using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Client.Request;
using Client.Request.Response.FileListResponse;
using Client.TencentCos.Task.List;
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
            FileRequest fileRequest = new FileRequest();
            int status = fileRequest.Download(File.Id, out FileListResponse res);

            switch (status)
            {
                case -20000:
                    TaskStatusDetectionThread.Abort();
                    DownloadTaskList.SetFailure(File.Key, "请求服务器失败，请稍后再试。");
                    break;
                case -20001:
                    TaskStatusDetectionThread.Abort();
                    DownloadTaskList.SetFailure(File.Key, "请求下载的文件不存在。");
                    break;
                case -10000:
                    TaskStatusDetectionThread.Abort();
                    DownloadTaskList.SetFailure(File.Key, res.message);
                    break;
                case 0:
                    {
                        string bucket = res.data.tencentCos.bucket;   //存储桶，格式：BucketName-APPID
                        string cosPath = res.data.files[0].storageName;    //对象在存储桶中的位置标识符，即称对象键
                        string localDir = File.LocalPath;   //本地文件夹
                        string localFileName = File.FileName;   //指定本地保存的文件名

                        CosService cosService = new CosService(res.data.tencentCos.region);
                        CosXml cosXml = cosService.getCosXml(res.data.token.credentials.tmpSecretId,res.data.token.credentials.tmpSecretKey,res.data.token.credentials.token,res.data.token.expiredTime);
                        TransferConfig transferConfig = new TransferConfig();
                        TransferManager transferManager = new TransferManager(cosXml, transferConfig);
                        downloadTask = new COSXMLDownloadTask(bucket, cosPath, localDir, localFileName);

                        downloadTask.progressCallback = delegate (long completed, long total)
                        {
                            DownloadTaskList.SetProgress(File.Key, completed, total);
                            Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                        };
                        downloadTask.successCallback = delegate (CosResult cosResult)
                        {
                            TaskStatusDetectionThread.Abort();
                            COSXMLDownloadTask.DownloadTaskResult result = cosResult as COSXMLDownloadTask.DownloadTaskResult;
                            Console.WriteLine("successCallback: " + result.GetResultInfo());
                            string eTag = result.eTag;
                            DownloadTaskList.SetSuccess(File.Key);
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
                            TaskStatusDetectionThread.Abort();
                            DownloadTaskList.SetFailure(File.Key, "COS下载出错。");
                        };
                        transferManager.Download(downloadTask);
                    }
                    break;
                default:
                    TaskStatusDetectionThread.Abort();
                    DownloadTaskList.SetFailure(File.Key, "未知原因下载失败。");
                    break;
            }
        }

        public void TaskStatusDetection()
        {
            while (true)
            {
                switch (DownloadTaskList.GetStatus(File.Key))
                {
                    case StatusType.RequestPause:
                        {
                            downloadTask.Pause();
                            DownloadTaskList.SetStatus(File.Key, StatusType.Paused);
                            break;
                        }
                    case StatusType.RequestRusume:
                        {
                            downloadTask.Resume();
                            DownloadTaskList.SetStatus(File.Key, StatusType.Running);
                            break;
                        }
                    case StatusType.RequestCancel:
                        {
                            downloadTask.Cancel();
                            DownloadTaskList.Remove(File.Key);
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
