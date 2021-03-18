namespace Gamebase.Models
{
    public class GamesCharacters
    {
        public GamesCharacters()
        {

        }
        public GamesCharacters(Game game, Character character)
        {
            Game = game;
            Character = character;
        }
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int CharacterId { get; set; }

        public Character Character { get; set; }
    }
}
