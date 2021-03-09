namespace Gamebase.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Newtonsoft.Json;

    public class InvolvedCompany
    {
        [JsonProperty("company")]
        public int CompanyId { get; set; }
    }
}
