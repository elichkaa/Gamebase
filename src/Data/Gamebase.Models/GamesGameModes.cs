namespace Gamebase.Models
{
    using System.Text.Json.Serialization;

    public class GamesGameModes
    {
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int GameModeId { get; set; }

        public GameMode GameMode { get; set; }
    }
}
