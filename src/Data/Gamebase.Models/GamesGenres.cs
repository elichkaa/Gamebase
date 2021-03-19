using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Models
{
    public class GamesGenres
    {
        public GamesGenres()
        {

        }
        public GamesGenres(int gameId, int genreId)
        {
            GameId = gameId;
            GenreId = genreId;
        }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
