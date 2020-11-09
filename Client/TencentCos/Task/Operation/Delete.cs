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
    public class Delete
    {
        private FCB File;

        public Delete(FCB file)
        {
            this.File = file;
        }

        public void Run()
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
