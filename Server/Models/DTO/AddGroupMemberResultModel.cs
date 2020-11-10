using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.DTO
{
    public class AddGroupMemberResultModel
    {
        public long GroupId { get; private set; }
        public long UserId { get; private set; }
        
        public AddGroupMemberResultModel(long groupid,long userid)
        {
            GroupId = groupid;
            UserId = userid;
        }
    }
}
