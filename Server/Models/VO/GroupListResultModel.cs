using System.Collections.Generic;
using System.Linq;
using Server.Models.Entities;

namespace Server.Models.VO
{
    class GroupListResultModel
    {
        public List<GroupModel> Users { get; private set; }

        public int Amount { get; private set; }

        public int Offset { get; private set; }

        public GroupListResultModel(IEnumerable<Group> groups, int amount, int offset)
        {
            this.Amount = groups.Count();
            this.Offset = offset;

            this.Users = new List<GroupModel>();
            foreach (var p in groups)
            {
                this.Users.Add(new GroupModel(p));
            }
        }
    }
}