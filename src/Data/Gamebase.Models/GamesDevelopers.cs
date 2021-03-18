namespace Gamebase.Models
{
    using System.ComponentModel.DataAnnotations;

    public class GamesDevelopers
    {
        public GamesDevelopers()
        {

        }
        public GamesDevelopers(Game game, Developer developer)
        {
            Game = game;
            Developer = developer;
        }
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int DeveloperId { get; set; }

        public Developer Developer { get; set; }
    }
}
