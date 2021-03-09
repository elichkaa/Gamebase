namespace Gamebase.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;

    public class Franchise : BaseEntity
    {
        public string Description { get; set; }

        [JsonProperty("games")]
        [NotMapped]
        public ICollection<int> GameIds { get; set; }

        [JsonIgnore]
        public ICollection<Game> Games { get; set; }

        public string GameIdsString
        {
            get
            {
                if (this.GameIds == null || this.GameIds.Count == 0)
                {
                    return null;
                }
                return string.Join(", ", this.GameIds);
            }
            set
            {

            }
        }
    }
}
