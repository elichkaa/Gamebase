namespace Gamebase.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class GameEngine : BaseEntity
    {
        public ICollection<GamesGameEngines> Games { get; set; }
    }
}
