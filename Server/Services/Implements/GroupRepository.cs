using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Models.Entities;

namespace Server.Services.Implements
{
    public class GroupRepository : IGroupRepository
    {

        private IDatabaseService _databaseService;
        public GroupRepository(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public Group? FindByIdWithPermissions(long id)
        {
            return _databaseService.Groups
                .Include(s => s.GroupToPermission)
                .FirstOrDefault(s => s.Id == id);
        }
    }
}
