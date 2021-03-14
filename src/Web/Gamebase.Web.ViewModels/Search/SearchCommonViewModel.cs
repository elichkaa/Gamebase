namespace Gamebase.Web.ViewModels.Search
{
    using System.Collections.Generic;
    using InputModels.Search;

    public class SearchCommonViewModel
    {
        public SearchCommonViewModel()
        {
            this.ViewModel = new List<SearchGameViewModel>();
        }
        public ICollection<SearchGameViewModel> ViewModel { get; set; }
        public SearchGameInputModel InputModel { get; set; }
    }
}
