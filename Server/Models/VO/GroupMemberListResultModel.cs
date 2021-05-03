using System.Collections.Generic;
using System.Linq;
using Server.Models.Entities;

namespace Server.Models.VO
{
    public class GroupMemberListResultModel
    {
        public Group Group { get; set; }
        public List<UserModel> Users { get; set; }
        public int RequestModelAmount { get; set; }
        public int RequestModelOffset { get; set; }

        public GroupMemberListResultModel(Group group, List<GroupToUser> users, int requestModelAmount, int requestModelOffset)
        {
            Group = group;
            Users = users.Select(s => new UserModel(s.User)).ToList();
            RequestModelAmount = users.Count();
            RequestModelOffset = requestModelOffset;
        }
    }
}