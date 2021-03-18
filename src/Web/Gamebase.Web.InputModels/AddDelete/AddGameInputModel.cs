using Gamebase.Models;
using Gamebase.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Web.InputModels.AddDelete
{
    public class AddGameInputModel
    {
        [Required]
        [Display(Name = "Game name")]
        public string Name { get; set; }

        [Display(Name = "Game cover/ optional")]
        public string Cover { get; set; }

        [Display(Name = "Game Storyline /optional")]
        public string Storyline { get; set; }

        [Display(Name = "Game Summary/ optional")]
        public string Summary { get; set; }

        [Required]
        [Display(Name = "Game release date")]
        public DateTime? FirstReleaseDate { get; set; }
        
        //Developer
        [Display(Name = "Developer names")]
        public string DeveloperName { get; set; }

        // Collection
        [Display(Name = "Collection name")]
        public string CollectionName { get; set; }

        // GameEngines
        [Display(Name = "Game engines")]
        public string GameEngineName { get; set; }

        // GameModes
        [Display(Name = "Game modes")]
        public string GameModeName { get; set; }

        // Genres
        [Display(Name = "Game genres")]
        public string GenreName { get; set; }

        // Keywords

        [Display(Name = "Game keywords")]
        public string KeywordName { get; set; }

        // Platforms

        [Display(Name = "Game platforms")]
        public string PlatformName { get; set; }

        // Screenshots
        [Display(Name = "Screenshot Urls")]
        public string ScreenshotUrl { get; set; }

        // Characters
        [Display(Name = "Character names")]
        public string CharacterName { get; set; }
    }
}
