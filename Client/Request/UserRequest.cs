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

        public LoginResponse Login(string username, string password)
        {
            var client = new RestClient(ServerAddress.Address + "/api/login");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var requestBody = new {username, password};
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            LoginResponse res = JsonConvert.DeserializeObject<LoginResponse>(response.Content);
            UserInfo.UserName = res.data.username;
            UserInfo.Token = res.data.token;
            return res;
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
