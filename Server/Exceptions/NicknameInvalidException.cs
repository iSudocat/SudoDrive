using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class NicknameInvalidException:APIException
    {
        public NicknameInvalidException(object data = null) : base(-114, "The nickname user entered is invalid", data)
        {

        }
    }
}
