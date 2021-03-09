namespace Gamebase.Models
{
    public class GamesPlayerPerspectives
    {
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int PlayerPerspectiveId { get; set; }

        public PlayerPerspective PlayerPerspective { get; set; }
    }
}
