using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class UsernameInvalidException: APIException
    {
        public UsernameInvalidException(object data = null) : base(-104, "The username user entered is invalid", data)
        {

        }
    }
}
