namespace Gamebase.Models
{
    public class GamesKeywords
    {
        public GamesKeywords()
        {

        }
        public GamesKeywords(int gameId, int keywordId)
        {
            GameId = gameId;
            KeywordId = keywordId;
        }
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int KeywordId { get; set; }

        public Keyword Keyword { get; set; }
    }
}
