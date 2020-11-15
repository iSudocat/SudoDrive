namespace Server.Exceptions
{
    public class BannedFromServerException : APIException
    {
        public BannedFromServerException (object data = null) : base(-2, "You are banned from this server.", data)
        {
        }
    }
}