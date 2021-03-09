namespace Gamebase.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;

    public class Video
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        //usually placed after watch?v= https://www.youtube.com/watch?v=here
        [JsonProperty("video_id")]
        public string VideoId { get; set; }

        [JsonProperty("game")]
        public int GameId { get; set; }

        [JsonIgnore]
        public Game Game { get; set; }
    }
}
