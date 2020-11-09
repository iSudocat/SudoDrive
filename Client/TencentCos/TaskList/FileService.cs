using Client.Request;
using COSXML;
using COSXML.CosException;
using COSXML.Model;
using COSXML.Model.Object;
using COSXML.Network;
using COSXML.Transfer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Client.TencentCos
{
    public class FileService
    {
        private FCB file;

        public FileService(FCB file)
        {
            this.file = file;
        }

        public void Test()
        {
            Thread.Sleep(3000);
            Console.WriteLine(file.FileName);
            FileTask.Remove(file.Key);
        }

        public void GetFileList()
        {
            //TODO 从后端获取文件列表
        }

        public void Upload()
        {
            String bucket = CosConfig.Bucket;   //存储桶，格式：BucketName-APPID

            string srcPath = file.LocalPath;    //本地文件绝对路径

            FileRequest fileRequest = new FileRequest();
            var res = fileRequest.Upload(srcPath, file.RemotePath);

            CosService cosService = new CosService();
            CosXml cosXml = cosService.getCosXml(
                res.data.token.credentials.tmpSecretId,
                res.data.token.credentials.tmpSecretKey,
                res.data.token.credentials.token,
                res.data.token.expiredTime);

            string cosPath = res.data.file.storageName;   //TODO 对象在存储桶中的位置标识符，即称对象键

            // 初始化 TransferConfig
            TransferConfig transferConfig = new TransferConfig();

            // 初始化 TransferManager
            TransferManager transferManager = new TransferManager(cosXml, transferConfig);

            PutObjectRequest putObjectRequest = new PutObjectRequest(bucket, cosPath, srcPath);

            //上传对象
            COSXMLUploadTask uploadTask = new COSXMLUploadTask(putObjectRequest);

            uploadTask.SetSrcPath(srcPath);

            uploadTask.progressCallback = delegate (long completed, long total)
            {
                FileTask.SetProgress(file.Key, completed, total);
                Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                switch (FileTask.GetStatus(file.Key))
                {
                    case StatusType.RequestPause:
                        {
                            uploadTask.Pause();
                            FileTask.SetStatus(file.Key, StatusType.Paused);
                            break;
                        }
                    case StatusType.RequestRusume:
                        {
                            uploadTask.Resume();
                            FileTask.SetStatus(file.Key, StatusType.Running);
                            break;
                        }
                    default:
                        break;
                }

            };
            uploadTask.successCallback = delegate (CosResult cosResult)
            {
                fileRequest.ConfirmUpload(res.data.file.id, res.data.file.guid);
                COSXMLUploadTask.UploadTaskResult result = cosResult as COSXMLUploadTask.UploadTaskResult;
                Console.WriteLine("successCallback " + result.GetResultInfo());
                string eTag = result.eTag;
                FileTask.Remove(file.Key);
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

        public void Download()
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

            // 初始化 TransferConfig
            TransferConfig transferConfig = new TransferConfig();

            // 初始化 TransferManager
            TransferManager transferManager = new TransferManager(cosXml, transferConfig);

            String bucket = CosConfig.Bucket; //存储桶，格式：BucketName-APPID
            String cosPath = file.RemotePath + "\\" + file.FileName; //对象在存储桶中的位置标识符，即称对象键
            string localDir = file.LocalPath;//本地文件夹
            string localFileName = file.FileName; //指定本地保存的文件名

            //下载对象
            COSXMLDownloadTask downloadTask = new COSXMLDownloadTask(bucket, cosPath, localDir, localFileName);

            downloadTask.progressCallback = delegate (long completed, long total)
            {
                FileTask.SetProgress(file.Key, completed, total);
                Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));

                switch (FileTask.GetStatus(file.Key))
                {
                    case StatusType.RequestPause:
                        {
                            downloadTask.Pause();
                            FileTask.SetStatus(file.Key, StatusType.Paused);
                            break;
                        }
                    case StatusType.RequestRusume:
                        {
                            downloadTask.Resume();
                            FileTask.SetStatus(file.Key, StatusType.Running);
                            break;
                        }
                    default:
                        break;
                }
            };
            downloadTask.successCallback = delegate (CosResult cosResult)
            {
                COSXMLDownloadTask.DownloadTaskResult result = cosResult as COSXMLDownloadTask.DownloadTaskResult;
                Console.WriteLine(result.GetResultInfo());
                string eTag = result.eTag;
                FileTask.Remove(file.Key);
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

        public void Delete()
        {
            /*
            try
            {
                string bucket = CosConfig.Bucket; //存储桶，格式：BucketName-APPID
                string key = file.RemotePath + "\\" + file.FileName; //对象键
                DeleteObjectRequest request = new DeleteObjectRequest(bucket, key);
                //执行请求
                DeleteObjectResult result = cosXml.DeleteObject(request);
                //请求成功
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
            */
        }
    }
}
