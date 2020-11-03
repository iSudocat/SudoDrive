using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class UnauthenticatedException : APIException
    {
        public UnauthenticatedException(object data = null) : base(-1, "Not logged in.", data)
        {
        }
    }
}
