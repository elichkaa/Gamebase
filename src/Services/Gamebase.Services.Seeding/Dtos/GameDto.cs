namespace Gamebase.Services.Scraping.Dtos
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Models.Enums;
    using Newtonsoft.Json;

    public class GameDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("age_ratings")]
        public ICollection<int> AgeRatings { get; set; }

        [JsonProperty("aggregated_rating")]
        public double? AggregatedRating { get; set; }

        [JsonProperty("total_rating")]
        public double? TotalRating { get; set; }

        [JsonProperty("alternative_names")]
        public ICollection<int> AlternativeNames { get; set; }

        [JsonProperty("artworks")]
        public ICollection<int> Artworks { get; set; }

        [JsonProperty("bundles")]
        public ICollection<int> Bundles { get; set; }

        [JsonProperty("category")]
        public GameCategoryEnum Category { get; set; }

        [JsonProperty("collection")]
        public int? CollectionId { get; set; }

        [JsonProperty("cover")]
        public int? CoverId { get; set; }

        //todo: convert from unix to normal datetime
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("dlcs")]
        public ICollection<int> DLCs { get; set; }

        [JsonProperty("expanded_games")]
        public ICollection<int> ExpandedGames { get; set; }

        [JsonProperty("expansions")]
        public ICollection<int> Expansions { get; set; }

        [JsonProperty("first_release_date")]
        public string FirstReleaseDate { get; set; }

        [JsonProperty("franchise")]
        public int? FranchiseId { get; set; }

        [JsonProperty("franchises")]
        public ICollection<int> Franchises { get; set; }

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

        [JsonProperty("multiplayer_modes")]
        public ICollection<int> MultiplayerModes { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parent_game")]
        public int? ParentGame { get; set; }

        [JsonProperty("player_perspectives")]
        public ICollection<int> PlayerPerspectives { get; set; }

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

        [JsonProperty("themes")]
        public ICollection<int> Themes { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("version_parent")]
        public int? VersionParent { get; set; }

        [JsonProperty("version_title")]
        public string VersionTitle { get; set; }

        [JsonProperty("videos")]
        public ICollection<int> Videos { get; set; }

        [JsonProperty("websites")]
        public ICollection<int> Websites { get; set; }
    }
}
