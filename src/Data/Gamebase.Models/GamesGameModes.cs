namespace Gamebase.Models
{
    using System.Text.Json.Serialization;

    public class GamesGameModes
    {
        public GamesGameModes()
        {

        }
        public GamesGameModes(Game game, GameMode gameMode)
        {
            Game = game;
            GameMode = GameMode;
        }
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int GameModeId { get; set; }

        public GameMode GameMode { get; set; }
    }
}
