using System;
using System.Linq;
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

        public override int SaveChanges()
        {
            var newEntities = this.ChangeTracker.Entries()
                .Where(
                    x => x.State == EntityState.Added &&
                         x.Entity != null &&
                         x.Entity as ITimeStampedModel != null
                )
                .Select(x => x.Entity as ITimeStampedModel);

            var modifiedEntities = this.ChangeTracker.Entries()
                .Where(
                    x => x.State == EntityState.Modified &&
                         x.Entity != null &&
                         x.Entity as ITimeStampedModel != null
                )
                .Select(x => x.Entity as ITimeStampedModel);


            foreach (var newEntity in newEntities)
            {
                if (newEntity == null) continue;
                newEntity.CreatedAt = DateTime.Now;
                newEntity.UpdatedAt = DateTime.Now;
            }

            foreach (var modifiedEntity in modifiedEntities)
            {
                if (modifiedEntity == null) continue;
                modifiedEntity.UpdatedAt = DateTime.Now;
            }

            return base.SaveChanges();
        }

    }
}
