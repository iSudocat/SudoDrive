using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Migrations
{
    public partial class CreateRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GroupsToUsersRelation_GroupId",
                table: "GroupsToUsersRelation",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsToPermissionsRelation_Groups_GroupId",
                table: "GroupsToPermissionsRelation",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsToUsersRelation_Groups_GroupId",
                table: "GroupsToUsersRelation",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsToUsersRelation_Users_UserId",
                table: "GroupsToUsersRelation",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupsToPermissionsRelation_Groups_GroupId",
                table: "GroupsToPermissionsRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupsToUsersRelation_Groups_GroupId",
                table: "GroupsToUsersRelation");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupsToUsersRelation_Users_UserId",
                table: "GroupsToUsersRelation");

            migrationBuilder.DropIndex(
                name: "IX_GroupsToUsersRelation_GroupId",
                table: "GroupsToUsersRelation");
        }
    }
}
