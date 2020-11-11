using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class GroupnameInvalidException:APIException
    {
        public GroupnameInvalidException(object data = null) : base(-112, "The groupname user entered is invalid", data)
        {

        }
    }
}
