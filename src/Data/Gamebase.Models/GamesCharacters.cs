namespace Gamebase.Models
{
    public class GamesCharacters
    {
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int CharacterId { get; set; }

        public Character Character { get; set; }
    }
}
