namespace Gamebase.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization.Formatters;
    using Newtonsoft.Json;

    //company in api
    public class Developer : BaseEntity
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("published")]
        [NotMapped]
        public ICollection<int> PublishedIds { get; set; }

        public ICollection<GamesDevelopers> Games { get; set; }

        public string PublishedGames
        {
            get
            {
                if (this.PublishedIds == null || this.PublishedIds.Count == 0)
                {
                    return null;
                }
                return string.Join(", ", this.PublishedIds);
            }
            set { }
        }

        [JsonProperty("parent")]
        public int ParentCompanyId { get; set; }
    }
}
