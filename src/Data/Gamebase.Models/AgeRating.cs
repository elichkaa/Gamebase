namespace Gamebase.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;
    using Newtonsoft.Json;

    public class AgeRating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("category")]
        public AgeRatingCategoryEnum Category { get; set; }

        [JsonProperty("rating")]
        public RatingEnum Rating { get; set; }

        [JsonProperty("content_descriptions")]
        public ICollection<ContentDescription> ContentDescriptions { get; set; }

        [JsonProperty("rating_cover_url")]
        public string RatingCoverUrl { get; set; }

        [JsonProperty("synopsis")]
        public string Synopsis { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }
    }
}
