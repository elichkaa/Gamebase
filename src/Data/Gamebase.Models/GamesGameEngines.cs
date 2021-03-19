namespace Gamebase.Models
{
    public class GamesGameEngines
    {
        public GamesGameEngines()
        {

        }
        public GamesGameEngines(int gameId, int gameEngineId)
        {
            GameId=gameId;
            GameEngineId = gameEngineId;
        }
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int GameEngineId { get; set; }

        public GameEngine GameEngine { get; set; }
    }
}
