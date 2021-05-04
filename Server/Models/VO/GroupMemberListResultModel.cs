using System.Collections.Generic;
using System.Linq;
using Server.Models.Entities;

namespace Server.Models.VO
{
    public class GroupMemberListResultModel
    {
        public GroupModel Group { get; init; }
        public List<UserModel> Users { get; init; }
        public int Amount { get; init; }
        public int Offset { get; init; }

        public GroupMemberListResultModel(Group group, List<GroupToUser> users, int requestModelAmount, int requestModelOffset)
        {
            Group = new GroupModel(group);
            Users = users.Select(s => new UserModel(s.User)).ToList();
            Amount = users.Count();
            Offset = requestModelOffset;
        }
    }
}