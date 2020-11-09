using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.DTO
{
    public class QuitGroupResultModel
    {
        public string _GroupName { get; set; }
        public QuitGroupResultModel(string groupName)
        {
            _GroupName = groupName;
        }
    }
}
