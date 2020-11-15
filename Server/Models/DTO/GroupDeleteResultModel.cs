using Server.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.VO;

namespace Server.Models.DTO
{
    public class GroupDeleteResultModel
    {
        public GroupModel Group { get; private set; }
        public GroupDeleteResultModel(Group group)
        {
            Group = group.ToVO();
        }
    }
}
