using System.Collections.Generic;
using Server.Models.Entities;

namespace Server.Models.VO
{
    class UserListResultModel
    {
        public List<CommonUserProfileResultModel> Users { get; private set; }

        public int Amount { get; private set; }

        public int Offset { get; private set; }

        public UserListResultModel(IEnumerable<User> users, int amount, int offset)
        {
            this.Amount = amount;
            this.Offset = offset;

            this.Users = new List<CommonUserProfileResultModel>();
            foreach (var p in users)
            {
                this.Users.Add(new CommonUserProfileResultModel(p));
            }
        }
    }
}