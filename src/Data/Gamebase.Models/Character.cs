using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<GameCharacter> GameCharacters { get; set; }
        public ICollection<DLCCharacter> DLCCharacters { get; set; }
        public ICollection<CharacterImage> CharacterImages{ get; set; }
    }
}
