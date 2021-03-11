namespace Gamebase.Models
{
    using Newtonsoft.Json;

    public abstract class BaseEntity : MainEntity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
