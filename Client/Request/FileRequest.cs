using Client.Request.Response.UploadResponse;
using Client.Request.Response.FileListResponse;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Client.Request
{
    public class FileRequest
    {
        public int Upload(string localPath, string remotePath, out UploadResponse uploadResponse)
        {
            string type = MimeMapping.GetMimeMapping(localPath);
            long size = new FileInfo(localPath).Length;
            string md5 = GetFileMD5(localPath);

            var client = new RestClient(ServerAddress.Address + "/api/storage/file");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + UserInfo.Token);
            request.AddHeader("Content-Type", "application/json");
            var requestBody = new { type, path = remotePath, size, md5 };
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            uploadResponse = JsonConvert.DeserializeObject<UploadResponse>(response.Content);
            if (uploadResponse != null)
            {
                return uploadResponse.status;
            }
            else
            {
                uploadResponse = null;
                return -114514;
            }
        }

        public int ConfirmUpload(long id, string guid)
        {
            var client = new RestClient(ServerAddress.Address + "/api/storage/file");
            var request = new RestRequest(Method.PATCH);
            request.AddHeader("Authorization", "Bearer " + UserInfo.Token);
            request.AddHeader("Content-Type", "application/json");
            var requestBody = new { id, guid };
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            if (response.Content.Length != 0)
            {
                var jObj = JObject.Parse(response.Content);
                return (int)jObj.SelectToken("$.status");
            }
            else
            {
                return -114514;
            }
        }

        public void GetFileList(string folder, out int status, out List<Response.FileListResponse.File> fileList)
        {
            fileList = new List<Response.FileListResponse.File>();
            int offset = 0;
            int currentAmount;
            do
            {
                var client = new RestClient(ServerAddress.Address + "/api/storage/file?&offset=" + offset + "&amount=100&folder=" + folder);
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer " + UserInfo.Token);
                request.AddHeader("Content-Type", "application/json");
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                FileListResponse fileListResponse = JsonConvert.DeserializeObject<FileListResponse>(response.Content);
                if (fileListResponse != null)
                {
                    currentAmount = fileListResponse.data.amount;
                    if (currentAmount != 0)
                    {
                        fileList.AddRange(fileListResponse.data.files);
                    }

                    offset = offset + currentAmount;  // 下一次请求用
                }
                else
                {
                    status = -114514;
                    return;
                }
            } while (currentAmount != 0);

            status = 0;
        }


        private static string GetFileMD5(string filePath)
        {
            FileStream file = new FileStream(filePath, FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
