using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Request
{
    public static class UserInfo
    {
        public static string UserName { get; set; } = "";
        public static string Token { get; set; } = "";
        public static string Nickname { get; set; } = "";
        public static List<Response.LoginResponse.Group> groups { get; set; } = new List<Response.LoginResponse.Group>();
    }
}
