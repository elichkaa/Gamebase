namespace Gamebase.Models
{
    public class GamesGameEngines
    {
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int GameEngineId { get; set; }

        public GameEngine GameEngine { get; set; }
    }
}
