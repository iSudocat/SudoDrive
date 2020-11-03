using Server.Models.Entities;

namespace Server.Services
{
    public interface IUserRepository
    {
        public User? FindById(long id);

        public User? FindByUsername(string username);

        public User? FindByIdWithGroupsAndPermissions(long id);
    }
}