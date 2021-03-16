namespace Gamebase.Web.ViewModels.Search
{
    using System.Collections.Generic;

    public class SearchDeveloperViewModel
    {
        public string Name { get; set; }

        public ICollection<SearchGameViewModel> GamesByDeveloper { get; set; }
    }
}
