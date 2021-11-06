using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Server.Models.Entities;

namespace Server.Services
{
    public interface IDatabaseService
    {
        public DbSet<File> Files { get; set; }
        public DbSet<FileRepository> FileRepository { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupToUser> GroupsToUsersRelation { get; set; }
        public DbSet<GroupToPermission> GroupsToPermissionsRelation { get; set; }
        public DbSet<UserToPermission> UserToPermissionRelation { get; set; }
        public int SaveChanges();
        public DatabaseFacade Database { get; }
    }
}