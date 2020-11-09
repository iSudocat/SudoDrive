using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class UserNotExistException:APIException
    {
        public UserNotExistException(object data = null) : base(-110, "User Does Not Exist.", data)
        {

        }
    }
}
