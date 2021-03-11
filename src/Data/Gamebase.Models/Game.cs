namespace Gamebase.Models
{
    using System;
    using System.Collections.Generic;
    using Enums;

    public class Game : BaseEntity
    {
        public double? TotalRating { get; set; }

        public string Bundles { get; set; }

        public GameCategoryEnum Category { get; set; }

        public int? CollectionId { get; set; }

        public Collection Collection { get; set; }

        public int? CoverId { get; set; }

        public Cover Cover { get; set; }

        public string Dlcs { get; set; }

        public string Expansions { get; set; }

        public DateTime? FirstReleaseDate { get; set; }

        public ICollection<GamesGameEngines> GameEngines { get; set; }

        public ICollection<GamesGameModes> GameModes { get; set; }

        public ICollection<GamesGenres> Genres { get; set; }

        public ICollection<GamesDevelopers> Developers { get; set; }

        public ICollection<GamesKeywords> Keywords { get; set; }

        public ICollection<GamesPlatforms> Platforms { get; set; }

        public ICollection<Screenshot> Screenshots { get; set; }

        public string SimilarGames { get; set; }

        public StatusEnum Status { get; set; }

        public string Storyline { get; set; }

        public string Summary { get; set; }

        public ICollection<Website> Websites { get; set; }

        public ICollection<GamesCharacters> Characters { get; set; }
    }
}
