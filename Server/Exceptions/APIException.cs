using System;

namespace Server.Exceptions
{
    public class APIException : Exception
    {
        public int Status { get; private set; }
        
        public APIException(int status, string message) : base(message)
        {
            this.Status = status;
        }
    };
}