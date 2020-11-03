using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations.MySqlDataBaseServiceMigrations
{
    public partial class InitialGroupSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var now = DateTime.Now;

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "GroupName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, now, "Admin", now},
                    { 2L, now, "User", now},
                    { 3L, now, "Guest", now}
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Password", "UpdatedAt", "Username" },
                values: new object[] { 1L, now, BCrypt.Net.BCrypt.HashPassword("admin"), now, "admin" });

            migrationBuilder.InsertData(
                table: "GroupsToPermissionsRelation",
                columns: new[] { "GroupId", "Permission" },
                values: new object[] { 1L, "*" });

            migrationBuilder.InsertData(
                table: "GroupsToUsersRelation",
                columns: new[] { "UserId", "GroupId" },
                values: new object[] { 1L, 1L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 1L, "*" });

            migrationBuilder.DeleteData(
                table: "GroupsToUsersRelation",
                keyColumns: new[] { "UserId", "GroupId" },
                keyValues: new object[] { 1L, 1L });

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
