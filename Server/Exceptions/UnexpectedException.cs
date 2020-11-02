using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class UnexpectedException : APIException
    {
        public UnexpectedException(object data = null) : base(-10000, "Unexpected Exception.", data)
        {
        }
    }
}
