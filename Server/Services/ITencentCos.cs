using System.Collections.Generic;
using Server.Models.Entities;

namespace Server.Services
{
    public interface ITencentCos
    {
        public Dictionary<string, object> GetUploadToken(File file);

        public Dictionary<string, object> GetDownloadToken(List<string> allowPrefix);
    }
}