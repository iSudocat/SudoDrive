using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations.MySqlDataBaseServiceMigrations
{
    public partial class InitialGroupPermissionSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "GroupsToPermissionsRelation",
                columns: new[] { "GroupId", "Permission" },
                values: new object[,]
                {
                    { 2L, "user.profile.changepassword" },
                    { 3L, "user.login" },
                    { 3L, "user.register" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 2L, "user.profile.changepassword" });

            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 3L, "user.login" });

            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 3L, "user.register" });
        }
    }
}
