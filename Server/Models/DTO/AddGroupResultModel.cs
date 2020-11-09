using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.Entities
{
    public class AddGroupResultModel
    {
        public Group _group { get; private set; }

        public AddGroupResultModel( Group group)
        {
            _group = group;
        }
    }
}
