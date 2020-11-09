using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.DTO
{
    public class DeleteGroupResultModel
    {
        public string _GroupName { get; private set; }
        public DeleteGroupResultModel(string groupname)
        {
            _GroupName = groupname;
        }
    }
}
