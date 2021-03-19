namespace Gamebase.Models
{
    using Newtonsoft.Json;

    public abstract class BaseImage : MainEntity
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("image_id")]
        public string ImageId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("game")]
        public int GameId { get; set; }

        [JsonIgnore]
        public Game Game { get; set; }

        [JsonIgnore]
        public string ApplicationUserId { get; set; }

        [JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
