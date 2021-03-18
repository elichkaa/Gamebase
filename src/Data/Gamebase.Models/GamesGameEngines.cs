namespace Gamebase.Models
{
    public class GamesGameEngines
    {
        public GamesGameEngines()
        {

        }
        public GamesGameEngines(Game game, GameEngine gameEngine)
        {
            Game = game;
            GameEngine = gameEngine;
        }
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int GameEngineId { get; set; }

        public GameEngine GameEngine { get; set; }
    }
}
