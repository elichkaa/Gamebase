using Microsoft.EntityFrameworkCore.Migrations;

namespace Gamebase.Data.Migrations
{
    public partial class NewDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamesGameEngines_GameEngines_GameEngineId",
                table: "GamesGameEngines");

            migrationBuilder.DropForeignKey(
                name: "FK_GamesGameEngines_Games_GameId",
                table: "GamesGameEngines");

            migrationBuilder.DropForeignKey(
                name: "FK_GamesGameModes_GameModes_GameModeId",
                table: "GamesGameModes");

            migrationBuilder.DropForeignKey(
                name: "FK_GamesGameModes_Games_GameId",
                table: "GamesGameModes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamesGameModes",
                table: "GamesGameModes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamesGameEngines",
                table: "GamesGameEngines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameEngines",
                table: "GameEngines");

            migrationBuilder.RenameTable(
                name: "GamesGameModes",
                newName: "GamesModes");

            migrationBuilder.RenameTable(
                name: "GamesGameEngines",
                newName: "GamesEngines");

            migrationBuilder.RenameTable(
                name: "GameEngines",
                newName: "GameEngine");

            migrationBuilder.RenameIndex(
                name: "IX_GamesGameModes_GameId",
                table: "GamesModes",
                newName: "IX_GamesModes_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GamesGameEngines_GameId",
                table: "GamesEngines",
                newName: "IX_GamesEngines_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamesModes",
                table: "GamesModes",
                columns: new[] { "GameModeId", "GameId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamesEngines",
                table: "GamesEngines",
                columns: new[] { "GameEngineId", "GameId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_GamesEngines_Games_GameId",
                table: "GamesEngines",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamesModes_GameModes_GameModeId",
                table: "GamesModes",
                column: "GameModeId",
                principalTable: "GameModes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamesModes_Games_GameId",
                table: "GamesModes",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamesEngines_GameEngine_GameEngineId",
                table: "GamesEngines");

            migrationBuilder.DropForeignKey(
                name: "FK_GamesEngines_Games_GameId",
                table: "GamesEngines");

            migrationBuilder.DropForeignKey(
                name: "FK_GamesModes_GameModes_GameModeId",
                table: "GamesModes");

            migrationBuilder.DropForeignKey(
                name: "FK_GamesModes_Games_GameId",
                table: "GamesModes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamesModes",
                table: "GamesModes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamesEngines",
                table: "GamesEngines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameEngine",
                table: "GameEngine");

            migrationBuilder.RenameTable(
                name: "GamesModes",
                newName: "GamesGameModes");

            migrationBuilder.RenameTable(
                name: "GamesEngines",
                newName: "GamesGameEngines");

            migrationBuilder.RenameTable(
                name: "GameEngine",
                newName: "GameEngines");

            migrationBuilder.RenameIndex(
                name: "IX_GamesModes_GameId",
                table: "GamesGameModes",
                newName: "IX_GamesGameModes_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_GamesEngines_GameId",
                table: "GamesGameEngines",
                newName: "IX_GamesGameEngines_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamesGameModes",
                table: "GamesGameModes",
                columns: new[] { "GameModeId", "GameId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamesGameEngines",
                table: "GamesGameEngines",
                columns: new[] { "GameEngineId", "GameId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameEngines",
                table: "GameEngines",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GamesGameEngines_GameEngines_GameEngineId",
                table: "GamesGameEngines",
                column: "GameEngineId",
                principalTable: "GameEngines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamesGameEngines_Games_GameId",
                table: "GamesGameEngines",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamesGameModes_GameModes_GameModeId",
                table: "GamesGameModes",
                column: "GameModeId",
                principalTable: "GameModes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamesGameModes_Games_GameId",
                table: "GamesGameModes",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
