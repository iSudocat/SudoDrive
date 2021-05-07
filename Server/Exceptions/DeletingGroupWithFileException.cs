using System.Collections.Generic;
using Server.Mixin;

namespace Server.Exceptions
{
    public class DeletingGroupWithFileException : APIException
    {
        public DeletingGroupWithFileException(string msg, string path) : base(-116, "The group you attempt to delete is not empty.",
            new Dictionary<string, object>().Also(
                s =>
                {
                    s.Add("message", msg);
                    s.Add("path", path);
                }))
        {
        }
    }
}