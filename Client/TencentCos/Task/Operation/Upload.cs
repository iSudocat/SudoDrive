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
using Client.Request.Response;
using COSXML.Auth;
using Client.Request.Response.UploadResponse;
using Client.TencentCos.Task.List;

namespace Client.TencentCos.Task.Operation
{
    public class Upload
    {
        private FileControlBlock File;
        private Thread TaskStatusDetectionThread;

        private COSXMLUploadTask uploadTask;

        public Upload(FileControlBlock file)
        {
            this.File = file;
            TaskStatusDetectionThread = new Thread(TaskStatusDetection);
            TaskStatusDetectionThread.Start();
        }

        public void Run()
        {
            string srcPath = File.LocalPath + "\\" + File.FileName;    //本地文件绝对路径

            FileRequest fileRequest = new FileRequest();
            int status = fileRequest.Upload(srcPath, File.RemotePath + "\\" + File.FileName, out UploadResponse res);

            switch (status)
            {
                case -20000:
                    TaskStatusDetectionThread.Abort();
                    UploadTaskList.SetFailure(File.Key, "请求服务器失败，请稍后再试。");
                    break;
                case -10000:
                    TaskStatusDetectionThread.Abort();
                    UploadTaskList.SetFailure(File.Key, res.message);
                    break;
                case 101:
                    TaskStatusDetectionThread.Abort();
                    UploadTaskList.SetProgress(File.Key, 1, 1);
                    UploadTaskList.SetSuccess(File.Key);
                    break;
                case 100:
                    {
                        string bucket = res.data.tencentCos.bucket;   //存储桶，格式：BucketName-APPID
                        string cosPath = res.data.file.storageName;   //对象在存储桶中的位置标识符，即称对象键

                        CosService cosService = new CosService(res.data.tencentCos.region);
                        CosXml cosXml = cosService.getCosXml(res.data.token.credentials.tmpSecretId, res.data.token.credentials.tmpSecretKey, res.data.token.credentials.token, res.data.token.expiredTime);
                        TransferConfig transferConfig = new TransferConfig();
                        TransferManager transferManager = new TransferManager(cosXml, transferConfig);
                        PutObjectRequest putObjectRequest = new PutObjectRequest(bucket, cosPath, srcPath);
                        uploadTask = new COSXMLUploadTask(putObjectRequest);

                        uploadTask.SetSrcPath(srcPath);

                        uploadTask.progressCallback = delegate (long completed, long total)
                        {
                            UploadTaskList.SetProgress(File.Key, completed, total);
                            Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                        };
                        uploadTask.successCallback = delegate (CosResult cosResult)
                        {
                            TaskStatusDetectionThread.Abort();
                            fileRequest.ConfirmUpload(res.data.file.id, res.data.file.guid);
                            COSXMLUploadTask.UploadTaskResult result = cosResult as COSXMLUploadTask.UploadTaskResult;
                            Console.WriteLine("successCallback: " + result.GetResultInfo());
                            string eTag = result.eTag;
                            UploadTaskList.SetSuccess(File.Key);
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
                            TaskStatusDetectionThread.Abort();
                            UploadTaskList.SetFailure(File.Key, "COS上传出错。");
                        };

                        transferManager.Upload(uploadTask);
                    }
                    break;
                default:
                    TaskStatusDetectionThread.Abort();
                    UploadTaskList.SetFailure(File.Key, "未知原因上传失败。");
                    break;
            }
        }

        public void TaskStatusDetection()
        {
            while (true)
            {
                switch (UploadTaskList.GetStatus(File.Key))
                {
                    case StatusType.RequestPause:
                        {
                            uploadTask.Pause();
                            UploadTaskList.SetStatus(File.Key, StatusType.Paused);
                            break;
                        }
                    case StatusType.RequestRusume:
                        {
                            uploadTask.Resume();
                            UploadTaskList.SetStatus(File.Key, StatusType.Running);
                            break;
                        }
                    case StatusType.RequestCancel:
                        {
                            uploadTask.Cancel();
                            UploadTaskList.Remove(File.Key);
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
