using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Models.Entities;

namespace Server.Services.Implements
{
    public class UserRepository : IUserRepository
    {

        private IDatabaseService _databaseService;
        public UserRepository(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public User? FindById(long id)
        {
            return _databaseService.Users.Find()
        }

        public User? FindByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public User? FindByIdWithGroupsAndPermissions(long id)
        {
            return _databaseService.Users
                .Include(s => s.GroupToUser)
                .ThenInclude(s => s.Group)
                .ThenInclude(s => s.GroupToPermission)
                .FirstOrDefault(s => s.Id == id);
        }
    }
}
