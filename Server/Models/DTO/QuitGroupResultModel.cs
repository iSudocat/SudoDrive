using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.DTO
{
    public class QuitGroupResultModel
    {
        public long GroupId { get;private set; }
        public long UserId { get;private set; }
        public QuitGroupResultModel(long groupid, long userid)
        {
            GroupId = groupid;
            UserId = userid;
        }
    }
}
