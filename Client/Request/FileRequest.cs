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
using Client.Request.Response.DeleteResponse;

namespace Client.Request
{
    public class FileRequest
    {
        public int Upload(string localPath, string remotePath, out UploadResponse uploadResponse)
        {
            if(UserInfo.UserName == "")
            {
                uploadResponse = null;
                return -1;
            }
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
                return -20000;
            }
        }

        public int ConfirmUpload(long id, string guid)
        {
            if (UserInfo.UserName == "")
            {
                return -1;
            }
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
                return -20000;
            }
        }

        public void GetFileList(string folder, out int status, out List<Response.FileListResponse.File> fileList)
        {
            if (UserInfo.UserName == "")
            {
                fileList = null;
                status = -1;
                return;
            }
            fileList = new List<Response.FileListResponse.File>();
            int offset = 0;
            int currentAmount;
            do
            {
                var client = new RestClient(ServerAddress.Address + "/api/storage/file?offset=" + offset + "&amount=100&folder=" + folder);
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
                    status = -20000;
                    return;
                }
            } while (currentAmount != 0);

            status = 0;
        }

        public int Download(string id, out FileListResponse fileListResponse)
        {
            if (UserInfo.UserName == "")
            {
                fileListResponse = null;
                return -1;
            }
            var client = new RestClient(ServerAddress.Address + "/api/storage/file?download=true&id=" + id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + UserInfo.Token);
            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            fileListResponse = JsonConvert.DeserializeObject<FileListResponse>(response.Content);
            if (fileListResponse != null)
            {
                if(fileListResponse.data.amount == 1)
                {
                    return fileListResponse.status;
                }
                else
                {
                    return -20001;
                }
            }
            else
            {
                return -20000;
            }
        }


        /// <summary>
        /// 删除文件（夹）
        /// </summary>
        /// <param name="path">欲删除的文件(夹)路径</param>
        /// <returns>状态码</returns>
        public int Delete(out DeleteResponse deleteResponse, string path)
        {
            if (UserInfo.UserName == "")
            {
                deleteResponse = null;
                return -1;
            }
            var client = new RestClient(ServerAddress.Address + "/api/storage/file");
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Authorization", "Bearer " + UserInfo.Token);
            request.AddHeader("Content-Type", "application/json");
            if (path != "")
            {
                var requestBody = new { path = new string[] { path } };
                request.AddParameter("application/json", JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);
            }
            else
            {
                deleteResponse = null;
                return -20002;
            }
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            deleteResponse = JsonConvert.DeserializeObject<DeleteResponse>(response.Content);
            if (deleteResponse != null)
            {
                if (deleteResponse.data.count == 0)
                {
                    return -20003;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return -20000;
            }
        }

        public int NewFolder(string folderPath, out UploadResponse uploadResponse)
        {
            if (UserInfo.UserName == "")
            {
                uploadResponse = null;
                return -1;
            }
            var client = new RestClient(ServerAddress.Address + "/api/storage/file");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + UserInfo.Token);
            request.AddHeader("Content-Type", "application/json");
            var requestBody = new { type = "text/directory", path = folderPath, size = 0, md5 = "00000000000000000000000000000000" };
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
                return -20000;
            }

        }

        public void SearchFile(string sth, out int status, out List<Response.FileListResponse.File> fileList)
        {
            if (UserInfo.UserName == "")
            {
                fileList = null;
                status = -1;
                return;
            }
            fileList = new List<Response.FileListResponse.File>();
            int offset = 0;
            int currentAmount;
            do
            {
                var client = new RestClient(ServerAddress.Address + "/api/storage/file?offset=" + offset + "&amount=100&nameContains=" + sth);
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
                    status = -20000;
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
