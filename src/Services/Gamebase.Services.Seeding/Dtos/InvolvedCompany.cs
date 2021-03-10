namespace Gamebase.Services.Seeding.Dtos
{
    using Newtonsoft.Json;

    public class InvolvedCompany
    {
        [JsonProperty("company")]
        public int CompanyId { get; set; }
    }
}
