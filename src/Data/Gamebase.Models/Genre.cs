using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Models
{
    public class Genre : BaseEntity
    {
        public ICollection<GamesGenres> GameGenres { get; set; }
    }
}
