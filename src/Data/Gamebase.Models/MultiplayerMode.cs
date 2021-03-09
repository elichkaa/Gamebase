namespace Gamebase.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;

    public class MultiplayerMode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("campaigncoop")]
        public bool CampaignCoop { get; set; }

        [JsonProperty("dropin")]
        public bool DropIn { get; set; }

        [JsonProperty("lancoop")]
        public bool LanCoop { get; set; }

        [JsonProperty("offlinecoop")]
        public bool OfflineCoop { get; set; }

        [JsonProperty("onlinecoop")]
        public bool OnlineCoop { get; set; }

        [JsonProperty("platform")]
        public int PlatformId { get; set; }

        [JsonIgnore]
        public Platform Platform { get; set; }

        [JsonProperty("game")]
        public int GameId { get; set; }

        [JsonIgnore]
        public Game Game { get; set; }
    }
}
