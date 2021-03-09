namespace Gamebase.Models
{
    using System.Collections.Generic;

    public class Keyword : BaseEntity
    {
        public ICollection<GamesKeywords> Games { get; set; }
    }
}
