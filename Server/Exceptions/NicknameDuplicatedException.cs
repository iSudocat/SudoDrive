using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class NicknameDuplicatedException:APIException
    {
        public NicknameDuplicatedException(object data = null) : base(-115, "Nickname Duplicate.", data)
        {
        }
    }
}
