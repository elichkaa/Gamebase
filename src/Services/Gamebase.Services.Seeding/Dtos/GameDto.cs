namespace Gamebase.Services.Seeding.Dtos
{
    using System.Collections.Generic;
    using Models;
    using Models.Enums;
    using Newtonsoft.Json;

    public class GameDto : BaseEntity
    {
        [JsonProperty("total_rating")]
        public double? TotalRating { get; set; }

        [JsonProperty("bundles")]
        public ICollection<int> Bundles { get; set; }

        [JsonProperty("category")]
        public GameCategoryEnum Category { get; set; }

        [JsonProperty("collection")]
        public int? CollectionId { get; set; }

        [JsonProperty("cover")]
        public int? CoverId { get; set; }

        [JsonProperty("dlcs")]
        public ICollection<int> DLCs { get; set; }

        [JsonProperty("expansions")]
        public ICollection<int> Expansions { get; set; }

        [JsonProperty("first_release_date")]
        public long FirstReleaseDate { get; set; }

        [JsonProperty("game_engines")]
        public ICollection<int> GameEngines { get; set; }

        [JsonProperty("game_modes")]
        public ICollection<int> GameModes { get; set; }

        [JsonProperty("genres")]
        public ICollection<int> Genres { get; set; }

        [JsonProperty("involved_companies")]
        public ICollection<int> InvolvedCompanies { get; set; }

        [JsonProperty("keywords")]
        public ICollection<int> Keywords { get; set; }

        [JsonProperty("platforms")]
        public ICollection<int> Platforms { get; set; }

        [JsonProperty("screenshots")]
        public ICollection<int> Screenshots { get; set; }

        [JsonProperty("similar_games")]
        public ICollection<int> SimilarGames { get; set; }

        [JsonProperty("status")]
        public StatusEnum Status { get; set; }

        [JsonProperty("storyline")]
        public string Storyline { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("websites")]
        public ICollection<int> Websites { get; set; }
    }
}
