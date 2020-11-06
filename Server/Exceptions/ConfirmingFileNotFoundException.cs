using System;

namespace Server.Exceptions
{
    public class ConfirmingFileNotFoundException : APIException
    {
        public ConfirmingFileNotFoundException(object data = null) : base(-102, "The file for confirming can not be found.", data)
        {
        }
    }
}