using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Request.Response.RegisterResponse
{
    public class RegisterResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {

    }
}
