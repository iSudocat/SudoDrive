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

        /// <summary>
        /// 所在共享组
        /// </summary>
        public static List<Response.LoginResponse.Group> Groups { get; set; } = new List<Response.LoginResponse.Group>();

    }
}
