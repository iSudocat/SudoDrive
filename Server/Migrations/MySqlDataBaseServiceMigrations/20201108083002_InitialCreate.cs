using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations.MySqlDataBaseServiceMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GroupName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Password = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupsToPermissionsRelation",
                columns: table => new
                {
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    Permission = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsToPermissionsRelation", x => new { x.GroupId, x.Permission });
                    table.ForeignKey(
                        name: "FK_GroupsToPermissionsRelation_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Type = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Folder = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Path = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Guid = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    StorageName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    Md5 = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Permission = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupsToUsersRelation",
                columns: table => new
                {
                    GroupId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsToUsersRelation", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_GroupsToUsersRelation_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupsToUsersRelation_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            var now = DateTime.Now;

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedAt", "GroupName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, now, "Admin",now },
                    { 2L, now, "User", now},
                    { 3L, now, "Guest",now }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Password", "UpdatedAt", "Username" },
                values: new object[] { 1L, now, BCrypt.Net.BCrypt.HashPassword("admin"), now, "admin" });

            migrationBuilder.InsertData(
                table: "GroupsToPermissionsRelation",
                columns: new[] { "GroupId", "Permission" },
                values: new object[,]
                {
                    { 1L, "*" },
                    { 2L, "user.auth.refresh" },
                    { 2L, "user.auth.updatepassword" },
                    { 2L, "storage.file.list.basic" },
                    { 2L, "storage.file.upload.basic" },
                    { 2L, "storage.file.delete.basic" },
                    { 3L, "user.auth.register" },
                    { 3L, "user.auth.login" }
                });

            migrationBuilder.InsertData(
                table: "GroupsToUsersRelation",
                columns: new[] { "GroupId", "UserId" },
                values: new object[] { 1L, 1L });

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

            migrationBuilder.CreateIndex(
                name: "IX_Files_UserId",
                table: "Files",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsToUsersRelation_GroupId",
                table: "GroupsToUsersRelation",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "GroupsToPermissionsRelation");

            migrationBuilder.DropTable(
                name: "GroupsToUsersRelation");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
