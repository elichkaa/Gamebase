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

        [Display(Name = "Game Url")]
        public string Url { get; set; }

        [Display(Name = "Game cover/ optional")]
        public string Cover { get; set; }

        [Display(Name = "Game Storyline /optional")]
        public string Storyline { get; set; }

        [Display(Name = "Game Summary/ optional")]
        public string Summary { get; set; }

        [Required]
        [Display(Name = "Game status")]
        public StatusEnum Status { get; set; }

        [Required]
        [Display(Name = "Game category")]
        public GameCategoryEnum Category { get; set; }
        [Required]
        [Display(Name = "Game release date")]
        public DateTime? FirstReleaseDate { get; set; }
        
        //Developer
        [Display(Name = "Developer name")]
        public string DeveloperName { get; set; }

        [Display(Name = "Developer Url")]
        public string DeveloperUrl { get; set; }

        [Display(Name = "Developer description")]
        public string DeveloperDescription { get; set; }

        // Collection
        [Display(Name = "Collection name")]
        public string CollectionName { get; set; }

        [Display(Name = "Collection Url")]
        public string CollectionUrl { get; set; }

        // GameEngines
        [Display(Name = "Game engine")]
        public string GameEngineName { get; set; }

        [Display(Name = "Game engine Url")]
        public string GameEngineUrl { get; set; }

        // GameModes
        [Display(Name = "Game mode")]
        public string GameModeName { get; set; }

        [Display(Name = "Game mode Url")]
        public string GameModeUrl { get; set; }

        // Genres
        [Display(Name = "Game genre")]
        public string GenreName { get; set; }

        [Display(Name = "Game genre Url")]
        public string GenreUrl { get; set; }
        // Keywords

        [Display(Name = "Game keyword")]
        public string KeywordName { get; set; }

        [Display(Name = "Game keyword Url")]
        public string KeywordUrl { get; set; }

        // Platforms

        [Display(Name = "Game platform")]
        public string PlatformName { get; set; }

        [Display(Name = "Game platform Url")]
        public string PlatformUrl { get; set; }

        // Screenshots
        [Display(Name = "Screenshot Url")]
        public string ScreenshotUrl { get; set; }

        // Websites
        [Display(Name = "Website Url")]
        public string WebsiteUrl { get; set; }

        // Characters
        [Display(Name = "Character name")]
        public string CharacterName { get; set; }
    }
}
