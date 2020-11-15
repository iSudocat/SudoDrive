using Server.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.DTO
{
    public class GroupMemberAddResultModel
    {
        public Group Group { get; private set; }
        public User User { get; private set; }
        
        public GroupMemberAddResultModel(Group group,User user)
        {
            Group = group;
            User = user;
        }
    }
}
