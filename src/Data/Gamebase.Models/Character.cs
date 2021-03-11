namespace Gamebase.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;

    public class Character : BaseEntity
    {
        [NotMapped]
        [JsonProperty("games")]
        public ICollection<int> GameIds { get; set; }

        [JsonIgnore]
        public ICollection<GamesCharacters> Games { get; set; }

        [JsonProperty("mug_shot")]
        public int ImageId{ get; set; }

        [JsonIgnore]
        public CharacterImage Image { get; set; }
    }
}
