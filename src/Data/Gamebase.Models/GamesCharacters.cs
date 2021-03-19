namespace Gamebase.Models
{
    public class GamesCharacters
    {
        public GamesCharacters()
        {

        }
        public GamesCharacters(int gameId, int characterId)
        {
            GameId = gameId;
            CharacterId = characterId;
        }
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int CharacterId { get; set; }

        public Character Character { get; set; }
    }
}
