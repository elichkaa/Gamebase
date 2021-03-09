namespace Gamebase.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class GameEngine : BaseEntity
    {
        public string Description { get; set; }

        [JsonIgnore]
        public DateTime ReleaseDate { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}
