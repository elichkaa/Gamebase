namespace Gamebase.Models
{
    public class GamesKeywords
    {
        public GamesKeywords()
        {

        }
        public GamesKeywords(Game game, Keyword keyword)
        {
            Game = game;
            Keyword = keyword;
        }
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int KeywordId { get; set; }

        public Keyword Keyword { get; set; }
    }
}
