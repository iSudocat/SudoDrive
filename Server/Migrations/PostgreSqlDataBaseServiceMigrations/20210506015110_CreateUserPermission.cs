using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations.PostgreSqlDataBaseServiceMigrations
{
    public partial class CreateUserPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_permission",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    permission = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_permission", x => new { x.user_id, x.permission });
                    table.ForeignKey(
                        name: "FK_user_permission_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "group_permission",
                columns: new[] { "group_id", "permission" },
                values: new object[] { 2L, "groupmanager.group.member.list.basic" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_permission");

            migrationBuilder.DeleteData(
                table: "group_permission",
                keyColumns: new[] { "group_id", "permission" },
                keyValues: new object[] { 2L, "groupmanager.group.member.list.basic" });
        }
    }
}
