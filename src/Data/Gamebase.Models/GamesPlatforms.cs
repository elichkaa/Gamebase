using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Models
{
    public class GamesPlatforms
    {
        public GamesPlatforms()
        {

        }
        public GamesPlatforms(Game game, Platform platform)
        {

        }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int PlatformId { get; set; }
        public Platform Platform { get; set; }
    }
}
