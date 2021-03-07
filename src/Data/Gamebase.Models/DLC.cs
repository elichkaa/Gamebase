using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Models
{
    public class DLC
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int Descritption { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Game Game { get; set; }
        public Developer Developer { get; set; }
        public ICollection<DLCCharacter> DLCCharacters { get; set; }
    }
}
