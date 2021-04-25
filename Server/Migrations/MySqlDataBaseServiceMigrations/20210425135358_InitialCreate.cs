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
                name: "groups",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    group_name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    password = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    nickname = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "group_permission",
                columns: table => new
                {
                    group_id = table.Column<long>(type: "bigint", nullable: false),
                    permission = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_permission", x => new { x.group_id, x.permission });
                    table.ForeignKey(
                        name: "FK_group_permission_groups_group_id",
                        column: x => x.group_id,
                        principalTable: "groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    type = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    folder = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    path = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    guid = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    storage_name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    md5 = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    permission = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    update_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_files", x => x.id);
                    table.ForeignKey(
                        name: "FK_files_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group_user",
                columns: table => new
                {
                    group_id = table.Column<long>(type: "bigint", nullable: false),
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_user", x => new { x.user_id, x.group_id });
                    table.ForeignKey(
                        name: "FK_group_user_groups_group_id",
                        column: x => x.group_id,
                        principalTable: "groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_group_user_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "groups",
                columns: new[] { "id", "create_at", "group_name", "update_at" },
                values: new object[,]
                {
                    { 1L, new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2L, new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User", new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3L, new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guest", new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "create_at", "nickname", "password", "status", "update_at", "username" },
                values: new object[] { 1L, new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "$2a$11$j9IgiAd3G7ZZKHF1vlr9M.dBnz0gzLNgO1M0ttnzbzn5QkdQpQ9Ga", null, new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin" });

            migrationBuilder.InsertData(
                table: "group_permission",
                columns: new[] { "group_id", "permission" },
                values: new object[,]
                {
                    { 1L, "*" },
                    { 2L, "user.auth.refresh" },
                    { 2L, "user.profile.basic" },
                    { 2L, "user.profile.update.basic" },
                    { 2L, "storage.file.list.basic" },
                    { 2L, "storage.file.upload.basic" },
                    { 2L, "storage.file.delete.basic" },
                    { 2L, "groupmanager.group.create.basic" },
                    { 2L, "groupmanager.group.delete.basic" },
                    { 2L, "groupmanager.group.quit.basic" },
                    { 2L, "groupmanager.group.member.add.basic" },
                    { 2L, "groupmanager.group.member.remove.basic" },
                    { 3L, "user.auth.register" },
                    { 3L, "user.auth.login" }
                });

            migrationBuilder.InsertData(
                table: "group_user",
                columns: new[] { "group_id", "user_id" },
                values: new object[] { 1L, 1L });

            migrationBuilder.CreateIndex(
                name: "IX_files_folder",
                table: "files",
                column: "folder");

            migrationBuilder.CreateIndex(
                name: "IX_files_guid",
                table: "files",
                column: "guid");

            migrationBuilder.CreateIndex(
                name: "IX_files_path",
                table: "files",
                column: "path");

            migrationBuilder.CreateIndex(
                name: "IX_files_status",
                table: "files",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_files_user_id",
                table: "files",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_user_group_id",
                table: "group_user",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                table: "users",
                column: "username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "files");

            migrationBuilder.DropTable(
                name: "group_permission");

            migrationBuilder.DropTable(
                name: "group_user");

            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
