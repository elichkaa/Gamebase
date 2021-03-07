using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Models
{
    public class DLCCharacter
    {
        public int DLCId { get; set; }
        public DLC DLC { get; set; }
        public int CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
