namespace Gamebase.Web.InputModels.AddDelete
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Microsoft.AspNetCore.Http;
    using Models;

    public class AddGameInputModel
    {
        public AddGameInputModel()
        {
            this.Screenshots = new List<IFormFile>();
        }
        [Required]
        [Display(Name = "Game name")]
        public string Name { get; set; }

        [Display(Name = "Game cover")]
        [DataType(DataType.Upload)]
        [Required]
        public IFormFile Cover { get; set; }

        [Display(Name = "Game Storyline /optional")]
        public string Storyline { get; set; }

        [Display(Name = "Game Summary/ optional")]
        public string Summary { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Game release date")]
        public DateTime FirstReleaseDate { get; set; }
        
        //Developer
        [Display(Name = "Developer names")]
        public string DeveloperNames { get; set; }

        // Collection
        [Display(Name = "Collection name")]
        public string CollectionName { get; set; }

        // GameEngines
        [Display(Name = "Game engines")]
        public string GameEngineNames { get; set; }

        // GameModes
        public List<int> GameModeIds { get; set; }

        [Display(Name = "Game modes")]
        public ICollection<GameMode> GameModes { get; set; }

        // Genres
        [Display(Name = "Game genres")]
        public string GenreNames { get; set; }

        // Keywords

        [Display(Name = "Game keywords")]
        public string KeywordNames { get; set; }

        // Platforms

        [Display(Name = "Game platforms")]
        public string PlatformNames { get; set; }

        // Screenshots
        [Display(Name = "Add screenshots")]
        [DataType(DataType.Upload)]
        public ICollection<IFormFile> Screenshots { get; set; }

        // Characters
        [Display(Name = "Character names")]
        public string CharacterNames { get; set; }
    }
}
