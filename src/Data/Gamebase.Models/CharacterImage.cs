namespace Gamebase.Models
{
    using Newtonsoft.Json;

    public class CharacterImage : MainEntity
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("image_id")]
        public string ImageId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        public int CharacterId { get; set; }

        public Character Character { get; set; }
    }
}
