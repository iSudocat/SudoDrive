namespace Server.Exceptions
{
    public class BannedFromServerException : APIException
    {
        public BannedFromServerException (object data = null) : base(-3, "You are banned from this server.", data)
        {
        }
    }
}