using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class AlterFileForPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_UserId",
                table: "Files");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Files",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Permission",
                table: "Files",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "GroupsToPermissionsRelation",
                columns: new[] { "GroupId", "Permission" },
                values: new object[,]
                {
                    { 2L, "storage.file.upload.basic" },
                    { 2L, "storage.file.delete.basic" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_Files_Folder",
                table: "Files",
                column: "Folder");

            migrationBuilder.CreateIndex(
                name: "IX_Files_Guid",
                table: "Files",
                column: "Guid");

            migrationBuilder.CreateIndex(
                name: "IX_Files_Path",
                table: "Files",
                column: "Path");

            migrationBuilder.CreateIndex(
                name: "IX_Files_Status",
                table: "Files",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_UserId",
                table: "Files",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_UserId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Files_Folder",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_Guid",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_Path",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_Status",
                table: "Files");

            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 2L, "storage.file.delete.basic" });

            migrationBuilder.DeleteData(
                table: "GroupsToPermissionsRelation",
                keyColumns: new[] { "GroupId", "Permission" },
                keyValues: new object[] { 2L, "storage.file.upload.basic" });

            migrationBuilder.DropColumn(
                name: "Permission",
                table: "Files");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "Files",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_UserId",
                table: "Files",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
