namespace Gamebase.Models
{
    public class GamesKeywords
    {
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int KeywordId { get; set; }

        public Keyword Keyword { get; set; }
    }
}
