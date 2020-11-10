using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.DTO
{
    public class DeleteGroupMemberResultModel
    {
        public long GroupId { get; private set; }
        public long UserId { get; private set; }
        public DeleteGroupMemberResultModel(long groupid,long userid)
        {
            GroupId = groupid;
            UserId = userid;
        }
    }
}
