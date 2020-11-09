using Client.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using COSXML;
using COSXML.CosException;
using COSXML.Model;
using COSXML.Model.Object;
using COSXML.Network;
using COSXML.Transfer;

namespace Client.TencentCos.Task.Operation
{
    public class Upload
    {
        private FCB File;
        private Thread TaskStatusDetectionThread;
        
        private COSXMLUploadTask uploadTask;

        public Upload(FCB file)
        {
            this.File = file;
            TaskStatusDetectionThread = new Thread(TaskStatusDetection);
            TaskStatusDetectionThread.Start();
        }

        public void Run()
        {
            string srcPath = File.LocalPath;    //本地文件绝对路径

            FileRequest fileRequest = new FileRequest();
            var res = fileRequest.Upload(srcPath, File.RemotePath);

            string bucket = CosConfig.Bucket;   //存储桶，格式：BucketName-APPID

            CosService cosService = new CosService();
            CosXml cosXml = cosService.getCosXml(
                res.data.token.credentials.tmpSecretId,
                res.data.token.credentials.tmpSecretKey,
                res.data.token.credentials.token,
                res.data.token.expiredTime);

            string cosPath = res.data.file.storageName;   //对象在存储桶中的位置标识符，即称对象键

            TransferConfig transferConfig = new TransferConfig();
            TransferManager transferManager = new TransferManager(cosXml, transferConfig);
            PutObjectRequest putObjectRequest = new PutObjectRequest(bucket, cosPath, srcPath);
            uploadTask = new COSXMLUploadTask(putObjectRequest);

            uploadTask.SetSrcPath(srcPath);

            uploadTask.progressCallback = delegate (long completed, long total)
            {
                TaskList.SetProgress(File.Key, completed, total);
                Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
            };
            uploadTask.successCallback = delegate (CosResult cosResult)
            {
                fileRequest.ConfirmUpload(res.data.file.id, res.data.file.guid);
                COSXMLUploadTask.UploadTaskResult result = cosResult as COSXMLUploadTask.UploadTaskResult;
                Console.WriteLine("successCallback: " + result.GetResultInfo());
                string eTag = result.eTag;

                TaskList.Remove(File.Key);

                TaskStatusDetectionThread.Abort();
            };
            uploadTask.failCallback = delegate (CosClientException clientEx, CosServerException serverEx)
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
            transferManager.Upload(uploadTask);
        }

        public void TaskStatusDetection()
        {
            while (true)
            {
                switch (TaskList.GetStatus(File.Key))
                {
                    case StatusType.RequestPause:
                        {
                            uploadTask.Pause();
                            TaskList.SetStatus(File.Key, StatusType.Paused);
                            break;
                        }
                    case StatusType.RequestRusume:
                        {
                            uploadTask.Resume();
                            TaskList.SetStatus(File.Key, StatusType.Running);
                            break;
                        }
                    case StatusType.RequestCancel:
                        {
                            uploadTask.Cancel();
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
