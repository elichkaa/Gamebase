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
        [Display(Name = "Developer name")]
        public string DeveloperName { get; set; }

        [Display(Name = "Developer description")]
        public string DeveloperDescription { get; set; }

        // Collection
        [Display(Name = "Collection name")]
        public string CollectionName { get; set; }

        // GameEngines
        [Display(Name = "Game engine")]
        public string GameEngineName { get; set; }

        // GameModes
        [Display(Name = "Game mode")]
        public string GameModeName { get; set; }

        // Genres
        [Display(Name = "Game genre")]
        public string GenreName { get; set; }

        // Keywords

        [Display(Name = "Game keyword")]
        public string KeywordName { get; set; }

        // Platforms

        [Display(Name = "Game platform")]
        public string PlatformName { get; set; }

        // Screenshots
        [Display(Name = "Screenshot Url")]
        public string ScreenshotUrl { get; set; }

        // Characters
        [Display(Name = "Character name")]
        public string CharacterName { get; set; }
    }
}
