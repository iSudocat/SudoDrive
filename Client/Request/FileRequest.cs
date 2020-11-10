using Client.Request.Response;
using Client.TencentCos;
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
        public UploadResponse Upload(string localPath, string remotePath)
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
            UploadResponse res = JsonConvert.DeserializeObject<UploadResponse>(response.Content);

            return res;
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
            UploadResponse res = JsonConvert.DeserializeObject<UploadResponse>(response.Content);
            var jObj = JObject.Parse(response.Content);
            return (int)jObj.SelectToken("$.status");
        }

        public FileListResponse GetFileList()
        {
            var client = new RestClient(ServerAddress.Address + "/api/storage/file");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + UserInfo.Token);
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            FileListResponse res = JsonConvert.DeserializeObject<FileListResponse>(response.Content);
            return res;
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
