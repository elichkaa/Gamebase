using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Models
{
    public class Character : BaseEntity
    {
        public string Description { get; set; }
        public ICollection<GameCharacter> GameCharacters { get; set; }
        public ICollection<CharacterImage> CharacterImages{ get; set; }
    }
}
