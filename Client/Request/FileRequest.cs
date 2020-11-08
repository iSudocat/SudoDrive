using Client.Request.Response;
using Newtonsoft.Json;
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
    public class FileService
    {
        public UploadResponse Upload(string filePath)
        {
            string type = MimeMapping.GetMimeMapping(filePath);
            long size = new FileInfo(filePath).Length;
            string md5 = GetFileMD5(filePath);

            var client = new RestClient(ServerAddress.Address + "/api/storage/file");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + UserInfo.Token);
            request.AddHeader("Content-Type", "application/json");
            var requestBody = new { type, path = filePath, size, md5 };
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            UploadResponse res = JsonConvert.DeserializeObject<UploadResponse>(response.Content);
            return res;
        }

        public static string GetFileMD5(string filePath)
        {
            FileStream file = new FileStream(filePath, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
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
