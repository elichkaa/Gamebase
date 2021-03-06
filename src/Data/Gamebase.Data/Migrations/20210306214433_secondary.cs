using Microsoft.EntityFrameworkCore.Migrations;

namespace Gamebase.Data.Migrations
{
    public partial class secondary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Developers_DeveloperId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_DeveloperId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "DeveloperId",
                table: "Developers",
                newName: "GameId");

            migrationBuilder.AddColumn<int>(
                name: "DeveloperId1",
                table: "Games",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_DeveloperId1",
                table: "Games",
                column: "DeveloperId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Developers_DeveloperId1",
                table: "Games",
                column: "DeveloperId1",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Developers_DeveloperId1",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_DeveloperId1",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "DeveloperId1",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Developers",
                newName: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_DeveloperId",
                table: "Games",
                column: "DeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Developers_DeveloperId",
                table: "Games",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
