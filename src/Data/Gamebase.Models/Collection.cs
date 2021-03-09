namespace Gamebase.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;

    public class Collection : BaseEntity
    {
        [NotMapped]
        [JsonProperty("games")]
        public ICollection<int> GameIds { get; set; }

        [JsonIgnore]
        public ICollection<Game> Games { get; set; }
    }
}
