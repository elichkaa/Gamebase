namespace Gamebase.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Enums;
    using Newtonsoft.Json;

    public class ContentDescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("category")]
        public AgeRatingCategoryEnum Category { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public int AgeRatingId { get; set; }

        public AgeRating AgeRating { get; set; }
    }
}
