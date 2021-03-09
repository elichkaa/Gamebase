using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Models
{
    using Enums;
    using Newtonsoft.Json;

    public class Platform : BaseEntity
    {
        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        [JsonProperty("abbreviation")]
        public string Abbreviation { get; set; }

        [JsonProperty("alternative_name")]
        public string AlternativeName { get; set; }

        [JsonProperty("category")]
        public PlatformCategoryEnum Category { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        public ICollection<GamesPlatforms> Games { get; set; }

        public ICollection<MultiplayerMode> MultiplayerModes { get; set; }
    }
}
