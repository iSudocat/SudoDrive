using System;

namespace Server.Exceptions
{
    public class FileNameIsEmptyException : APIException
    {
        public FileNameIsEmptyException(object data = null) : base(-107, "File name is empty.", data)
        {
        }
    }
}