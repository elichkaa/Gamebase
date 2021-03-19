namespace Gamebase.Models
{
    using System.ComponentModel.DataAnnotations;

    public class GamesDevelopers
    {
        public GamesDevelopers()
        {

        }
        public GamesDevelopers(int gameId, int developerId)
        {
            GameId = gameId;
            DeveloperId = developerId;
        }
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int DeveloperId { get; set; }

        public Developer Developer { get; set; }
    }
}
