using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Models
{
    public class GameCharacter
    {
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
