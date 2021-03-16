namespace Gamebase.Data.Services.Contracts
{
    using Web.ViewModels.Developers;
    using System.Collections.Generic;

    using Web.InputModels.Search;
    using Web.ViewModels.Search;

    public interface IDeveloperService
    {
        public DeveloperOnSinglePageViewModel GetSingleDeveloper(string name);

        public ICollection<SearchDeveloperViewModel> GetDeveloperByName(SearchDeveloperInputModel input);
    }
}
