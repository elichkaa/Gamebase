namespace Gamebase.Models
{
    public class CharacterImage : BaseImage
    {
        public int CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
