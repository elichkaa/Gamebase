using Microsoft.EntityFrameworkCore.Migrations;

namespace Gamebase.Data.Migrations
{
    public partial class RemoveGameIdFromCharacterImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterImages_Games_GameId",
                table: "CharacterImages");

            migrationBuilder.DropIndex(
                name: "IX_CharacterImages_GameId",
                table: "CharacterImages");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "CharacterImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "CharacterImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CharacterImages_GameId",
                table: "CharacterImages",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterImages_Games_GameId",
                table: "CharacterImages",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
