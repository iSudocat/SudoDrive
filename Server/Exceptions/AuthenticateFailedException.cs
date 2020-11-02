using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class AuthenticateFailedException : APIException
    {
        public AuthenticateFailedException(object data = null) : base(-2, "Authenticate Failed.", data)
        {
        }
    }
}
