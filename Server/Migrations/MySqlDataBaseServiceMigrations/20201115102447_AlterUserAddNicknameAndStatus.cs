using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations.MySqlDataBaseServiceMigrations
{
    public partial class AlterUserAddNicknameAndStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 2L, "user.auth.updatepassword" });

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "Users",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "GroupsToPermissionsRelation",
                columns: new[] { "GroupId", "Permission" },
                values: new object[,]
                {
                    { 2L, "user.profile.basic" },
                    { 2L, "user.profile.update.basic" },
                    { 2L, "groupmanager.group.create.basic" },
                    { 2L, "groupmanager.group.delete.basic" },
                    { 2L, "groupmanager.group.quit.basic" },
                    { 2L, "groupmanager.group.member.add.basic" },
                    { 2L, "groupmanager.group.member.remove.basic" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 2L, "groupmanager.group.create.basic" });

            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 2L, "groupmanager.group.delete.basic" });

            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 2L, "groupmanager.group.member.add.basic" });

            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 2L, "groupmanager.group.member.remove.basic" });

            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 2L, "groupmanager.group.quit.basic" });

            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 2L, "user.profile.basic" });

            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 2L, "user.profile.update.basic" });

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.InsertData(
                table: "GroupsToPermissionsRelation",
                columns: new[] { "GroupId", "Permission" },
                values: new object[] { 2L, "user.auth.updatepassword" });
        }
    }
}
