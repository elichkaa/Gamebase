﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Models
{
    public class Developer
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int GameId { get; set; }
        public DeveloperLogo DeveloperLogo { get; set; }
        public ICollection<Game> Games;
    }
}
