using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class GroupnameNotExistException: APIException
    {
        public GroupnameNotExistException(object data = null) : base(-107, "Groupname Does Not Exist.", data)
        {

        }
    }
}
