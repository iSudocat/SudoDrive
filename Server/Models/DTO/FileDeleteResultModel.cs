
using System.Collections.Generic;
using Server.Models.Entities;
using Server.Models.VO;

namespace Server.Models.DTO
{
    public class FileDeleteResultModel
    {
        public long Count { get; private set; }

        public FileDeleteResultModel(long count)
        {
            this.Count = count;
        }
    }
}
