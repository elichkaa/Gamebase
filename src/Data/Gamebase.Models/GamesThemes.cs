namespace Gamebase.Models
{
    public class GamesThemes
    {
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int ThemeId { get; set; }

        public Theme Theme { get; set; }
    }
}
