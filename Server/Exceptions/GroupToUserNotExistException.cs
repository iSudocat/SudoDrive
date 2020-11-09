using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class GroupToUserNotExistException : APIException
    {
        public GroupToUserNotExistException(object data = null) : base(-109, "This grouptouser does not exist.", data)
        {

        }
    }
}
