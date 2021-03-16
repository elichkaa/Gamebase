namespace Gamebase.Data.Services
{
    using Gamebase.Models;
    using Gamebase.Web.InputModels.AddDelete;
    using System.Collections.Generic;
    using Web.InputModels.Search;
    using Web.ViewModels.Games;
    using Web.ViewModels.Home;
    using Web.ViewModels.Search;

    public interface IGamesService
    {
        public List<GameOnAllPageViewModel> GetAll(int currentPage);
        public GameOnDetailsPageViewModel GetSingle(int id);
        public ICollection<SearchGameViewModel> GetGame(SearchGameInputModel input);
        public void AddGame(AddGameInputModel input);
        public void DeleteGame(DeleteGameInputModel input);
        public decimal GetMaxPages();
        public List<GameOnHomePageViewModel> GetThreeMostRecentGames();
        public List<GameOnHomePageViewModel> GetFourRandomGames();
    }
}
