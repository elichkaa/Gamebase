namespace Gamebase.Models
{
    using System.Text.Json.Serialization;

    public class GamesGameModes
    {
        public GamesGameModes()
        {

        }
        public GamesGameModes(int gameId, int gameModeId)
        {
            GameId = gameId;
            GameModeId = gameModeId;
        }
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int GameModeId { get; set; }

        public GameMode GameMode { get; set; }
    }
}
