using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Request.Response.RefreshTokenResponse;
using Client.Request.Response.RegisterResponse;
using Client.Request.Response.LoginResponse;
using Client.TencentCos;
using Newtonsoft.Json;
using RestSharp;

namespace Client.Request
{
    public class UserRequest
    {
        public int Login(string username, string password, out LoginResponse loginResponse)
        {
            var client = new RestClient(ServerAddress.Address + "/api/login");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var requestBody = new { username, password };
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);
            if (loginResponse != null)
            {
                if (loginResponse.status == 0)
                {
                    UserInfo.UserName = loginResponse.data.username;
                    UserInfo.Token = loginResponse.data.token;
                    UserInfo.Nickname = loginResponse.data.user.nickname;
                    UserInfo.Groups = loginResponse.data.groups;
                }
                return loginResponse.status;
            }
            else
            {
                loginResponse = null;
                return -20000;
            }
        }

        public int RefreshToken()
        {
            var client = new RestClient(ServerAddress.Address + "/api/profile/refreshlogintoken");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + UserInfo.Token);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            RefreshTokenResponse res = JsonConvert.DeserializeObject<RefreshTokenResponse>(response.Content);
            return res.status;
        }

        public int Register(string username, string password, string nickname, out RegisterResponse registerResponse)
        {
            var client = new RestClient(ServerAddress.Address + "/api/register");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var requestBody = new { username, password, nickname };
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            registerResponse = JsonConvert.DeserializeObject<RegisterResponse>(response.Content);
            if (registerResponse != null)
            {
                return registerResponse.status;
            }
            else
            {
                registerResponse = null;
                return -20000;
            }
        }

    }
}
