using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Models
{
    public class DeveloperLogo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool Animated { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public int DeveloperId { get; set; }
        public Developer Developer { get; set; }

    }
}
