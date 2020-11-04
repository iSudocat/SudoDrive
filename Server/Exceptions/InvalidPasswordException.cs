using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class InvalidPasswordException:APIException
    {
        public InvalidPasswordException(object data = null) : base(-102, "Password Pattern Invalid.", data)
        {

        }
    }
}
