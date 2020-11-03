using Server.Models.Entities;

namespace Server.Services
{
    public interface IGroupRepository
    {
        public Group? FindByIdWithPermissions(long id);
    }
}