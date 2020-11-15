using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models.Entities;
using Server.Models.VO;

namespace Server.Models.DTO
{
    public class GetAttributesResultModel
    {
        public UserModel User { get; private set; }

        public GetAttributesResultModel(User p)
        {
            User = p.ToVO();
        }

    }
}
