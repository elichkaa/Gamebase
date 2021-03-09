namespace Gamebase.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;

    public class AlternativeName 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("game")]
        public int GameId { get; set; }

        [JsonIgnore]
        public Game Game { get; set; }

    }
}
