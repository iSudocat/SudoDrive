using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class UsernameDuplicatedException : APIException
    {
        public UsernameDuplicatedException(object data = null) : base(-101, "Username Duplicate.", data)
        {
        }
    }
}
