using Server.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.DTO
{
    public class DeleteGroupResultModel
    {
        public Group Group { get; private set; }
        public DeleteGroupResultModel(Group group)
        {
            Group = group;
        }
    }
}
