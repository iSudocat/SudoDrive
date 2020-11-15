using Server.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.VO;

namespace Server.Models.DTO
{
    public class GroupMemberRemoveResultModel
    {
        public GroupModel Group { get; private set; }
        public UserModel User { get; private set; }
        public GroupMemberRemoveResultModel(Group group,User user)
        {
            Group = group.ToVO();
            User = user.ToVO();
        }
    }
}
