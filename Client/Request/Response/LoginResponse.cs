using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Request.Response.LoginResponse
{
    public class LoginResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string username { get; set; }
        public string token { get; set; }
        public User user { get; set; }
        public List<Group> groups { get; set; }
    }

    public class User
    {
        public string id { get; set; }
        public string username { get; set; }
        public string nickname { get; set; }
    }

    public class Group
    {
        public string id { get; set; }
        public string groupName { get; set; }
    }
}
