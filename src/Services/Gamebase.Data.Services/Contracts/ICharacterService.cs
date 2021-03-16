namespace Gamebase.Data.Services.Contracts
{
    using Web.InputModels.Search;
    using Web.ViewModels.Search;
    using Gamebase.Web.ViewModels.Characters;
    using System.Collections.Generic;

    public interface ICharacterService
    {
        public List<CharacterOnAllPageViewModel> GetAll(int currentPage);

        public ICollection<SearchCharacterViewModel> GetCharacterByName(SearchCharacterInputModel input);

        public decimal GetMaxPages();
    }
}
