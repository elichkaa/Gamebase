namespace Gamebase.Models
{
    using System.Collections.Generic;
    using Enums;
    using Newtonsoft.Json;

    public class Platform : BaseEntity
    {
        [JsonProperty("abbreviation")]
        public string Abbreviation { get; set; }

        [JsonProperty("alternative_name")]
        public string AlternativeName { get; set; }

        [JsonProperty("category")]
        public PlatformCategoryEnum Category { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        public ICollection<GamesPlatforms> Games { get; set; }
    }
}
