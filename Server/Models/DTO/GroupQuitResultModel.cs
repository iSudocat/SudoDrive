using Server.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.VO;

namespace Server.Models.DTO
{
    public class GroupQuitResultModel
    {
        public GroupModel Group { get; private set; }
        public UserModel User { get; private set; }
        public GroupQuitResultModel(Group group, User user)
        {
            Group = group.ToVO();
            User = user.ToVO();
        }
    }
}