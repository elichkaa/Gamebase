namespace Gamebase.Models
{
    using System.Collections.Generic;

    public class Theme : BaseEntity
    {
        public ICollection<GamesThemes> Games { get; set; }
    }
}
