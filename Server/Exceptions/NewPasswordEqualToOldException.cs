using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    public class NewPasswordEqualToOldException:APIException
    {
        public NewPasswordEqualToOldException(object data = null) : base(-105, "The new password is equal to the older one.", data)
        {

        }
    }
}
