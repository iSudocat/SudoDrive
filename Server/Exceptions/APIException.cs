using System;

namespace Server.Exceptions
{
    public class APIException : Exception
    {
        public int Status { get; private set; }

        public object ApiExceptionData { get; private set; }

        public APIException(int status, string message, object data = null) : base(message)
        {
            this.Status = status;
            this.ApiExceptionData = data;
        }
    };
}