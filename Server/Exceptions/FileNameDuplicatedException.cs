using System;

namespace Server.Exceptions
{
    public class FileNameDuplicatedException : APIException
    {
        public FileNameDuplicatedException(object data = null) : base(-103, "The file name was already taken by other record.", data)
        {
        }
    }
}