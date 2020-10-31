using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class InvalidArgumentException : APIException
    {
        public InvalidArgumentException(object data = null) : base(-100, "Argument Invalid.", data)
        {
        }
    }
}
