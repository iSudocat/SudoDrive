using System;
using Server.Models.Entities;

namespace Server.Models.VO
{
    public class GroupModel
    {
        public long Id { get; private init; }
        public string GroupName { get; private init; }

        public DateTime CreatedAt { get; private init; }

        public DateTime UpdatedAt { get; private init; }

        public GroupModel(Group group)
        {
            this.Id = group.Id;
            this.GroupName = group.GroupName;
            this.CreatedAt = group.CreatedAt;
            this.UpdatedAt = group.UpdatedAt;
        }

    }
}