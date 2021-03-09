using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gamebase.Models
{
    using Enums;

    public class Game : MainEntity
    {
        public ICollection<AgeRating> AgeRatings { get; set; }

        public double? AggregatedRating { get; set; }

        public double? TotalRating { get; set; }

        public ICollection<AlternativeName> AlternativeNames { get; set; }

        public ICollection<Artwork> Artworks { get; set; }

        public string Bundles { get; set; }

        public GameCategoryEnum Category { get; set; }

        public int? CollectionId { get; set; }

        public Collection Collection { get; set; }

        public int? CoverId { get; set; }

        public Cover Cover { get; set; }

        //todo: convert from unix to normal datetime
        public string CreatedAt { get; set; }

        public string Dlcs { get; set; }

        public string ExpandedGames { get; set; }

        public string Expansions { get; set; }

        public string FirstReleaseDate { get; set; }

        public int? FranchiseId { get; set; }

        public Franchise MainFranchise { get; set; }

        public ICollection<GamesGameEngines> GameEngines { get; set; }

        public ICollection<GamesGameModes> GameModes { get; set; }

        public ICollection<GamesGenres> Genres { get; set; }

        public ICollection<GamesDevelopers> Developers { get; set; }

        public ICollection<GamesKeywords> Keywords { get; set; }

        public ICollection<MultiplayerMode> MultiplayerModes { get; set; }

        public ICollection<GamesPlayerPerspectives> PlayerPerspectives { get; set; }

        public ICollection<GamesPlatforms> Platforms { get; set; }

        public ICollection<Screenshot> Screenshots { get; set; }

        public string SimilarGames { get; set; }

        public StatusEnum Status { get; set; }

        public string Storyline { get; set; }

        public string Summary { get; set; }

        public ICollection<GamesThemes> Themes { get; set; }

        public string Url { get; set; }

        public int? VersionParent { get; set; }

        public string VersionTitle { get; set; }

        public ICollection<Video> Videos { get; set; }

        public ICollection<Website> Websites { get; set; }

        [MaxLength(70)]
        public string Name { get; set; }

        public int? ParentGameId { get; set; }

        public ICollection<GameCharacter> Characters { get; set; }
    }
}
