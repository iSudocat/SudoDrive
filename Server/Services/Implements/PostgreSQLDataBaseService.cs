using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Server.Models.VO;
using Server.Models.Entities;

namespace Server.Services.Implements
{
    public class PostgreSqlDataBaseService : DbContext, IDatabaseService
    {
        private DatabaseManagementModel _connectionInfo;

        public PostgreSqlDataBaseService(IOptions<DatabaseManagementModel> databaseManagementModel)
        {
            _connectionInfo = databaseManagementModel.Value;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql(_connectionInfo.ConnectionInfo);

        public DbSet<File> Files { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupToUser> GroupsToUsersRelation { get; set; }
        public DbSet<GroupToPermission> GroupsToPermissionsRelation { get; set; }
    }
}
