using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations.MySqlDataBaseServiceMigrations
{
    public partial class AddFileListPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_GroupId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_GroupId",
                table: "Files");

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

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "PermissionName",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "Folder",
                table: "Files",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Md5",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Files",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Files",
                nullable: false);

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "Files",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Files",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StorageName",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Files",
                nullable: false);

            migrationBuilder.InsertData(
                table: "GroupsToPermissionsRelation",
                columns: new[] { "GroupId", "Permission" },
                values: new object[,]
                {
                    { 2L, "user.auth.refresh" },
                    { 2L, "user.auth.updatepassword" },
                    { 2L, "storage.file.list.basic" },
                    { 3L, "user.auth.register" },
                    { 3L, "user.auth.login" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_UserId",
                table: "Files",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_UserId",
                table: "Files",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_UserId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_UserId",
                table: "Files");

            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 2L, "storage.file.list.basic" });

            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 2L, "user.auth.refresh" });

            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 2L, "user.auth.updatepassword" });

            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 3L, "user.auth.login" });

            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 3L, "user.auth.register" });

            migrationBuilder.DropColumn(
                name: "Folder",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Md5",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "StorageName",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Files",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Files",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "Files",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "GroupId",
                table: "Files",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermissionName",
                table: "Files",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "GroupsToPermissionsRelation",
                columns: new[] { "GroupId", "Permission" },
                values: new object[,]
                {
                    { 2L, "user.profile.changepassword" },
                    { 3L, "user.login" },
                    { 3L, "user.register" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_GroupId",
                table: "Files",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_GroupId",
                table: "Files",
                column: "GroupId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
