using System;
using Server.Models.Entities;

namespace Server.Models.VO
{
    public class GroupModel
    {
        public long Id { get; private set; }
        public string GroupName { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public GroupModel(Group group)
        {
            this.Id = group.Id;
            this.GroupName = group.GroupName;
            this.CreatedAt = group.CreatedAt;
            this.UpdatedAt = group.UpdatedAt;
        }

    }
}