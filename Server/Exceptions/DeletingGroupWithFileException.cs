using System.Collections.Generic;
using Server.Mixin;

namespace Server.Exceptions
{
    public class DeletingGroupWithFileException : APIException
    {
        public DeletingGroupWithFileException(string msg, string path) : base(-107, "Groupname Does Not Exist.",
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