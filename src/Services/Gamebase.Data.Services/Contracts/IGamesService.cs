namespace Gamebase.Data.Services
{
    using System.Collections.Generic;
    using Web.ViewModels.Games;
    using Web.ViewModels.Search;

    public interface IGamesService
    {
        public List<GameOnAllPageViewModel> GetAll(int currentPage);
        public GameOnDetailsPageViewModel GetSingle(int id);

        public ICollection<SearchGameViewModel> GetGameByName(string name, string developerName);
    }
}
