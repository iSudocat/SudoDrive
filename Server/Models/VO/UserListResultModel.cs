using System.Collections.Generic;
using System.Linq;
using Server.Models.Entities;

namespace Server.Models.VO
{
    class UserListResultModel
    {
        public List<DetailedUserProfileResultModel> Users { get; private set; }

        public int Amount { get; private set; }

        public int Offset { get; private set; }

        public UserListResultModel(IEnumerable<User> users, int amount, int offset)
        {
            this.Amount = users.Count();
            this.Offset = offset;

            this.Users = new List<DetailedUserProfileResultModel>();
            foreach (var p in users)
            {
                this.Users.Add(new DetailedUserProfileResultModel(p));
            }
        }
    }
}