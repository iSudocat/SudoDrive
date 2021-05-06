using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Server.Libraries;
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
        public DbSet<UserToPermission> UserToPermissionRelation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 表结构
            modelBuilder.Entity<GroupToPermission>()
                .HasKey(c => new {c.GroupId, c.Permission});

            modelBuilder.Entity<GroupToPermission>()
                .HasOne(s => s.Group)
                .WithMany(s => s.GroupToPermission)
                .HasForeignKey(sc => sc.GroupId);
            
            modelBuilder.Entity<UserToPermission>()
                .HasKey(c => new {c.UserId, c.Permission});

            modelBuilder.Entity<UserToPermission>()
                .HasOne(s => s.User)
                .WithMany(s => s.UserToPermission)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<GroupToUser>()
                .HasKey(c => new {c.UserId, c.GroupId});

            modelBuilder.Entity<GroupToUser>()
                .HasOne(s => s.User)
                .WithMany(s => s.GroupToUser)
                .HasForeignKey(sc => sc.UserId);


            modelBuilder.Entity<GroupToUser>()
                .HasOne(s => s.Group)
                .WithMany(s => s.GroupToUser)
                .HasForeignKey(sc => sc.GroupId);

            modelBuilder.Entity<User>()
                .HasIndex(s => new {s.Username});

            modelBuilder.Entity<File>()
                .HasIndex(s => new {s.Path});

            modelBuilder.Entity<File>()
                .HasIndex(s => new {s.Folder});

            modelBuilder.Entity<File>()
                .HasIndex(s => new {s.Guid});

            modelBuilder.Entity<File>()
                .HasIndex(s => new {s.Status});

            // 初始化数据
            var now = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            modelBuilder.Entity<Group>()
                .HasData(
                    new {Id = Group.GroupID.ADMIN, GroupName = "Admin", CreatedAt = now, UpdatedAt = now },
                    new {Id = Group.GroupID.DEFAULT, GroupName = "User", CreatedAt = now, UpdatedAt = now },
                    new {Id = Group.GroupID.GUEST, GroupName = "Guest", CreatedAt = now, UpdatedAt = now }
                );

            // default password : adminadmin
            modelBuilder.Entity<User>()
                .HasData(
                    new {Id = 1L, Username = "admin", Password = "$2a$11$j9IgiAd3G7ZZKHF1vlr9M.dBnz0gzLNgO1M0ttnzbzn5QkdQpQ9Ga", CreatedAt = now, UpdatedAt = now}
                );

            modelBuilder.Entity<GroupToUser>()
                .HasData(
                    new {UserId = 1L, GroupId = 1L}
                );

            modelBuilder.Entity<GroupToPermission>()
                .HasData(
                    new { GroupId = Group.GroupID.ADMIN, Permission = "*" },

                    new { GroupId = Group.GroupID.DEFAULT, Permission = PermissionBank.UserAuthRefresh },
                    new { GroupId = Group.GroupID.DEFAULT, Permission = PermissionBank.UserProfileBasic },
                    new { GroupId = Group.GroupID.DEFAULT, Permission = PermissionBank.UserProfileUpdateBasic },
                    new { GroupId = Group.GroupID.DEFAULT, Permission = PermissionBank.StorageFileListBasic },
                    new { GroupId = Group.GroupID.DEFAULT, Permission = PermissionBank.StorageFileUploadBasic },
                    new { GroupId = Group.GroupID.DEFAULT, Permission = PermissionBank.StorageFileDeleteBase },
                    new { GroupId = Group.GroupID.DEFAULT, Permission = PermissionBank.GroupManageGroupCreateBasic },
                    new { GroupId = Group.GroupID.DEFAULT, Permission = PermissionBank.GroupManageGroupDeleteBasic },
                    new { GroupId = Group.GroupID.DEFAULT, Permission = PermissionBank.GroupManageGroupQuitBasic },
                    new { GroupId = Group.GroupID.DEFAULT, Permission = PermissionBank.GroupManageGroupMemberAddBasic },
                    new { GroupId = Group.GroupID.DEFAULT, Permission = PermissionBank.GroupManageGroupMemberRemoveBasic },
                    new { GroupId = Group.GroupID.DEFAULT, Permission = PermissionBank.GroupManageGroupMemberListBasic },

                    new { GroupId = Group.GroupID.GUEST, Permission = PermissionBank.UserAuthRegister },
                    new { GroupId = Group.GroupID.GUEST, Permission = PermissionBank.UserAuthLogin }
                );
        }

        public override int SaveChanges()
        {
            var now = DateTime.Now;

            var newEntities = this.ChangeTracker.Entries()
                .Where(
                    x => x.State == EntityState.Added
                         && x.Entity is ICreateTimeStampedModel
                )
                .Select(x => x.Entity as ICreateTimeStampedModel);

            var modifiedEntities = this.ChangeTracker.Entries()
                .Where(
                    x => (x.State == EntityState.Modified || x.State == EntityState.Added)
                         && x.Entity is IUpdateTimeStampedModel
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