﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Models
{
    public class Game
    {

        public int Id { get; set; }

        [MaxLength(70)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public bool HasMultiplayer { get; set; }
        public int DeveloperId { get; set; }
        public Developer Developer { get; set; }
        public int FranchiseId { get; set; }
        public Franchise Franchise { get; set; }
        public int CoverId { get; set; }
        public Cover Cover { get; set; }
        public int GameEngineId { get; set; }
        public GameEngine GameEngine { get; set; }
        public ICollection<Screenshot> Screenshots { get; set; }
        
        public ICollection<GameGenre> GameGenres { get; set; }
        public ICollection<GamePlatform> GamePlatforms { get; set; }
        public ICollection<DLC> DLCs { get; set; }
        public ICollection<GameCharacter> GameCharacters { get; set; }
    }
}
