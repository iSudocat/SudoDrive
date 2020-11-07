using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class InvalidArgumentException : APIException
    {
        public static int ConstStatus = -100;
        public static string ConstMessage = "Argument Invalid.";

        public InvalidArgumentException(object data = null) : base(ConstStatus, ConstMessage, data)
        {
        }
    }
}
