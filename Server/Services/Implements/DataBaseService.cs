using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Server.Models.VO;
using Server.Models.Entities;

namespace Server.Services.Implements
{
    public abstract class DataBaseService : DbContext, IDatabaseService
    {
        public DbSet<File> Files { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupToUser> GroupsToUsersRelation { get; set; }
        public DbSet<GroupToPermission> GroupsToPermissionsRelation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupToPermission>()
                .HasKey(c => new { c.GroupId, c.Permission });

            modelBuilder.Entity<GroupToUser>()
                .HasKey(c => new { c.UserId, c.GroupId });
        }

    }
}
