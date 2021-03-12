using Gamebase.Models;
using Gamebase.Models.Enums;
using System;
using System.Collections.Generic;

namespace Gamebase.Web.ViewModels.Games
{
    public class GameOnDetailsPageViewModel
    {
        public ICollection<Developer> Developers { get; set; }
        public double? TotalRating { get; set; }

        public string Bundles { get; set; }

        public GameCategoryEnum Category { get; set; }

        public Collection Collection { get; set; }

        public Cover Cover { get; set; }

        public string Dlcs { get; set; }

        public string Expansions { get; set; }

        public DateTime? FirstReleaseDate { get; set; }

        public ICollection<GameEngine> GameEngines { get; set; }

        public ICollection<GameMode> GameModes { get; set; }

        public ICollection<string> Genres { get; set; }

        public ICollection<Keyword> Keywords { get; set; }

        public ICollection<Platform> Platforms { get; set; }

        public ICollection<Screenshot> Screenshots { get; set; }

        public string SimilarGames { get; set; }

        public StatusEnum Status { get; set; }

        public string Storyline { get; set; }

        public string Summary { get; set; }

        public ICollection<Website> Websites { get; set; }

        public ICollection<Character> Characters { get; set; }
    }
}
