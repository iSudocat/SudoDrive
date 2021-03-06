﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Server.Services.Implements;

namespace Server.Migrations.PostgreSqlDataBaseServiceMigrations
{
    [DbContext(typeof(PostgreSqlDataBaseService))]
    [Migration("20210425135358_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Server.Models.Entities.File", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_at");

                    b.Property<string>("Folder")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("folder");

                    b.Property<string>("Guid")
                        .HasColumnType("text")
                        .HasColumnName("guid");

                    b.Property<string>("Md5")
                        .HasColumnType("text")
                        .HasColumnName("md5");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("path");

                    b.Property<string>("Permission")
                        .HasColumnType("text")
                        .HasColumnName("permission");

                    b.Property<long>("Size")
                        .HasColumnType("bigint")
                        .HasColumnName("size");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("StorageName")
                        .HasColumnType("text")
                        .HasColumnName("storage_name");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("update_at");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("Folder");

                    b.HasIndex("Guid");

                    b.HasIndex("Path");

                    b.HasIndex("Status");

                    b.HasIndex("UserId");

                    b.ToTable("files");
                });

            modelBuilder.Entity("Server.Models.Entities.Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_at");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("group_name");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("update_at");

                    b.HasKey("Id");

                    b.ToTable("groups");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            GroupName = "Admin",
                            UpdatedAt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2L,
                            CreatedAt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            GroupName = "User",
                            UpdatedAt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3L,
                            CreatedAt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            GroupName = "Guest",
                            UpdatedAt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Server.Models.Entities.GroupToPermission", b =>
                {
                    b.Property<long>("GroupId")
                        .HasColumnType("bigint")
                        .HasColumnName("group_id");

                    b.Property<string>("Permission")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("permission");

                    b.HasKey("GroupId", "Permission");

                    b.ToTable("group_permission");

                    b.HasData(
                        new
                        {
                            GroupId = 1L,
                            Permission = "*"
                        },
                        new
                        {
                            GroupId = 2L,
                            Permission = "user.auth.refresh"
                        },
                        new
                        {
                            GroupId = 2L,
                            Permission = "user.profile.basic"
                        },
                        new
                        {
                            GroupId = 2L,
                            Permission = "user.profile.update.basic"
                        },
                        new
                        {
                            GroupId = 2L,
                            Permission = "storage.file.list.basic"
                        },
                        new
                        {
                            GroupId = 2L,
                            Permission = "storage.file.upload.basic"
                        },
                        new
                        {
                            GroupId = 2L,
                            Permission = "storage.file.delete.basic"
                        },
                        new
                        {
                            GroupId = 2L,
                            Permission = "groupmanager.group.create.basic"
                        },
                        new
                        {
                            GroupId = 2L,
                            Permission = "groupmanager.group.delete.basic"
                        },
                        new
                        {
                            GroupId = 2L,
                            Permission = "groupmanager.group.quit.basic"
                        },
                        new
                        {
                            GroupId = 2L,
                            Permission = "groupmanager.group.member.add.basic"
                        },
                        new
                        {
                            GroupId = 2L,
                            Permission = "groupmanager.group.member.remove.basic"
                        },
                        new
                        {
                            GroupId = 3L,
                            Permission = "user.auth.register"
                        },
                        new
                        {
                            GroupId = 3L,
                            Permission = "user.auth.login"
                        });
                });

            modelBuilder.Entity("Server.Models.Entities.GroupToUser", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint")
                        .HasColumnName("group_id");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("group_user");

                    b.HasData(
                        new
                        {
                            UserId = 1L,
                            GroupId = 1L
                        });
                });

            modelBuilder.Entity("Server.Models.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_at");

                    b.Property<string>("Nickname")
                        .HasColumnType("text")
                        .HasColumnName("nickname");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<int?>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("update_at");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("Username");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "$2a$11$j9IgiAd3G7ZZKHF1vlr9M.dBnz0gzLNgO1M0ttnzbzn5QkdQpQ9Ga",
                            UpdatedAt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("Server.Models.Entities.File", b =>
                {
                    b.HasOne("Server.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Server.Models.Entities.GroupToPermission", b =>
                {
                    b.HasOne("Server.Models.Entities.Group", "Group")
                        .WithMany("GroupToPermission")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Server.Models.Entities.GroupToUser", b =>
                {
                    b.HasOne("Server.Models.Entities.Group", "Group")
                        .WithMany("GroupToUser")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Server.Models.Entities.User", "User")
                        .WithMany("GroupToUser")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Server.Models.Entities.Group", b =>
                {
                    b.Navigation("GroupToPermission");

                    b.Navigation("GroupToUser");
                });

            modelBuilder.Entity("Server.Models.Entities.User", b =>
                {
                    b.Navigation("GroupToUser");
                });
#pragma warning restore 612, 618
        }
    }
}
