using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Request.Response.refreshToken;
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
            var requestBody = new {username, password};
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);
            if (loginResponse != null)
            {
                if(loginResponse.status == 0)
                {
                    UserInfo.UserName = loginResponse.data.username;
                    UserInfo.Token = loginResponse.data.token;
                }
                return loginResponse.status;
            }
            else
            {
                loginResponse = null;
                return -20000;
            }
        }

        public int refreshToken()
        {
            var client = new RestClient(ServerAddress.Address + "/api/profile/refreshlogintoken");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer " + UserInfo.Token);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            refreshTokenResponse res = JsonConvert.DeserializeObject<refreshTokenResponse>(response.Content);
            return res.status;
        }

    }
}
