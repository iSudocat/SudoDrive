using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class GroupnameDuplicatedException : APIException
    {
        public GroupnameDuplicatedException(object data = null) : base(-106, "Groupname Duplicate.", data)
        {

        }
    }
}
