using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Request.Response.RefreshTokenResponse
{
    public class RefreshTokenResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string username { get; set; }
        public string token { get; set; }
    }
}
