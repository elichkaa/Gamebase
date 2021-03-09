namespace Gamebase.Models
{
    using System.Collections.Generic;

    public class PlayerPerspective : BaseEntity
    {
       public ICollection<GamesPlayerPerspectives> Games { get; set; }
    }
}
