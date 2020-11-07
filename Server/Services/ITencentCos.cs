using System.Collections.Generic;
using Server.Models.Entities;

namespace Server.Services
{
    public interface ITencentCos
    {
        public Dictionary<string, object> GetToken(File file);
    }
}