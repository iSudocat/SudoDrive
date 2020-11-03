using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class RegisterFailedException : APIException
    {
        public RegisterFailedException(object data = null) : base(-101, "Register Failed.", data)
        {
        }
    }
}
