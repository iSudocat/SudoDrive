using System;
using System.Collections.Generic;
using Server.Models.Entities;
using Server.Models.VO;

namespace Server.Models.DTO
{
    public class GetAttributesResultModel
    {
        public UserModel User { get; private set; }

        public List<GroupModel> Groups { get; private set; }

        public GetAttributesResultModel(User p)
        {
            User = p.ToVO();

            Groups = new List<GroupModel>();
            foreach (var t in p.GroupToUser)
            {
                Groups.Add(t.Group.ToVO());
            }
        }
    }
}
