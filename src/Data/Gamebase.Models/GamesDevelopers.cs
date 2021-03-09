namespace Gamebase.Models
{
    using System.ComponentModel.DataAnnotations;

    public class GamesDevelopers
    {
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int DeveloperId { get; set; }

        public Developer Developer { get; set; }
    }
}
