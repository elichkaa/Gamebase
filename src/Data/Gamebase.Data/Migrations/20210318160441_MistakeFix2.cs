using Microsoft.EntityFrameworkCore.Migrations;

namespace Gamebase.Data.Migrations
{
    public partial class MistakeFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamesEngines_GameEngine_GameEngineId",
                table: "GamesEngines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameEngine",
                table: "GameEngine");

            migrationBuilder.RenameTable(
                name: "GameEngine",
                newName: "GameEngines");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameEngines",
                table: "GameEngines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GamesEngines_GameEngines_GameEngineId",
                table: "GamesEngines",
                column: "GameEngineId",
                principalTable: "GameEngines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamesEngines_GameEngines_GameEngineId",
                table: "GamesEngines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameEngines",
                table: "GameEngines");

            migrationBuilder.RenameTable(
                name: "GameEngines",
                newName: "GameEngine");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameEngine",
                table: "GameEngine",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GamesEngines_GameEngine_GameEngineId",
                table: "GamesEngines",
                column: "GameEngineId",
                principalTable: "GameEngine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
