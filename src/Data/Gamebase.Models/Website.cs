namespace Gamebase.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;
    using Newtonsoft.Json;

    public class Website
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("category")]
        public WebsiteCategoryEnum Category { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("trusted")]
        public string IsTrusted { get; set; }

        [JsonProperty("game")]
        public int GameId { get; set; }

        [JsonIgnore]
        public Game Game { get; set; }
    }
}
