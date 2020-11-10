using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class GroupToUserAlreadyExistException :APIException
    {
        public GroupToUserAlreadyExistException(object data = null) : base(-111, "grouptouser already exists.", data)
        {

        }
    }
}
