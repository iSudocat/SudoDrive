using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models.DTO
{
    public class DeleteGroupMemberResultModel
    {
        public string _GroupName { get; private set; }
        public string _UserName { get; private set; }
        public DeleteGroupMemberResultModel(string groupname,string username)
        {
            _GroupName = groupname;
            _UserName = username;
        }
    }
}
