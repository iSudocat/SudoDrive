using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.DTO
{
    public class DeleteGroupResultModel
    {
        public long GroupId { get; private set; }
        public DeleteGroupResultModel(long groupid)
        {
            GroupId = groupid;
        }
    }
}
