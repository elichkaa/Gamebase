using Gamebase.Models;
using Gamebase.Models.Enums;
using System;
using System.Collections.Generic;

namespace Gamebase.Web.ViewModels.Games
{
    public class GameOnDetailsPageViewModel
    {
        public string Name { get; set; } 

        public ICollection<string> Developers { get; set; }

        public string AverageRating { get; set; }

        public string Bundles { get; set; } 

        public GameCategoryEnum Category { get; set; }

        public Collection Collection { get; set; } 

        public string Cover { get; set; }

        public string Dlcs { get; set; } 

        public string Expansions { get; set; }

        public DateTime? FirstReleaseDate { get; set; }

        public ICollection<string> GameEngines { get; set; }

        public ICollection<GameMode> GameModes { get; set; }

        public ICollection<Genre> Genres { get; set; }

        public ICollection<string> Keywords { get; set; }

        public ICollection<string> Platforms { get; set; }

        public ICollection<string> Screenshots { get; set; }

        public string SimilarGames { get; set; }

        public StatusEnum Status { get; set; }

        public string Storyline { get; set; }

        public string Summary { get; set; }

        public ICollection<Website> Websites { get; set; }
    }
}
