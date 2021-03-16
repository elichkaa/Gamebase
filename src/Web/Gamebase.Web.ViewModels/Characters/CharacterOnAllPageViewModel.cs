using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamebase.Web.ViewModels.Characters
{
    public class CharacterOnAllPageViewModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
