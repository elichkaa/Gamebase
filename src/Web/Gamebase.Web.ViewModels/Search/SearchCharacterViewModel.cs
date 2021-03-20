namespace Gamebase.Web.ViewModels.Search
{
    using System.Collections.Generic;

    public class SearchCharacterViewModel
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public ICollection<SearchGameViewModel> Games { get; set; }


    }
}
