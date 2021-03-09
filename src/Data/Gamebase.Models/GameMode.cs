namespace Gamebase.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class GameMode : BaseEntity
    {
        [JsonProperty("game_modes")]
        public virtual ICollection<GamesGameModes> Games { get; set; }
    }
}
