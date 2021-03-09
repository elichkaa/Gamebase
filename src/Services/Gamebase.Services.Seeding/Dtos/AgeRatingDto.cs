namespace Gamebase.Services.Scraping.Dtos
{
    using System.Collections.Generic;
    using Models;
    using Models.Enums;
    using Newtonsoft.Json;

    public class AgeRatingDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("category")]
        public AgeRatingCategoryEnum Category { get; set; }

        [JsonProperty("rating")]
        public RatingEnum Rating { get; set; }

        [JsonProperty("content_descriptions")]
        public ICollection<int> ContentDescriptions { get; set; }

        [JsonProperty("rating_cover_url")]
        public string RatingCoverUrl { get; set; }

        [JsonProperty("synopsis")]
        public string Synopsis { get; set; }
    }
}
