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
            var now = DateTime.Now;

            var newEntities = this.ChangeTracker.Entries()
                .Where(
                    x => x.State == EntityState.Added &&
                         x.Entity != null &&
                         x.Entity as ICreateTimeStampedModel != null
                )
                .Select(x => x.Entity as ICreateTimeStampedModel);

            var modifiedEntities = this.ChangeTracker.Entries()
                .Where(
                    x => (x.State == EntityState.Modified || x.State == EntityState.Added) &&
                         x.Entity != null &&
                         x.Entity as IUpdateTimeStampedModel != null
                )
                .Select(x => x.Entity as IUpdateTimeStampedModel);


            foreach (var newEntity in newEntities)
            {
                if (newEntity == null) continue;
                newEntity.CreatedAt = now;
            }

            foreach (var modifiedEntity in modifiedEntities)
            {
                if (modifiedEntity == null) continue;
                modifiedEntity.UpdatedAt = now;
            }

            return base.SaveChanges();
        }

    }
}
