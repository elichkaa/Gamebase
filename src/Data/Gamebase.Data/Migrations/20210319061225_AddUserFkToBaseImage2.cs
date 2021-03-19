using Microsoft.EntityFrameworkCore.Migrations;

namespace Gamebase.Data.Migrations
{
    public partial class AddUserFkToBaseImage2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Screenshots",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Covers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Screenshots_ApplicationUserId",
                table: "Screenshots",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Covers_ApplicationUserId",
                table: "Covers",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Covers_AspNetUsers_ApplicationUserId",
                table: "Covers",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Screenshots_AspNetUsers_ApplicationUserId",
                table: "Screenshots",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Covers_AspNetUsers_ApplicationUserId",
                table: "Covers");

            migrationBuilder.DropForeignKey(
                name: "FK_Screenshots_AspNetUsers_ApplicationUserId",
                table: "Screenshots");

            migrationBuilder.DropIndex(
                name: "IX_Screenshots_ApplicationUserId",
                table: "Screenshots");

            migrationBuilder.DropIndex(
                name: "IX_Covers_ApplicationUserId",
                table: "Covers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Screenshots");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Covers");
        }
    }
}
