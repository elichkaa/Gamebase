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

        public int AddGame(AddGameInputModel input, ApplicationUser user, string basePath);

        public void DeleteGame(int gameId);

        public decimal GetMaxPages();

        public List<GameOnHomePageViewModel> GetThreeMostRecentGames();

        public List<GameOnHomePageViewModel> GetFourRandomGames();

        public ICollection<GameMode> GetGameModes();

        public ICollection<GameOnAllPageViewModel> GetGamesByUser(string userId);

        public bool InputFieldIsNull(string field);

        public int GetBiggestId<T>() where T : MainEntity;

        public bool CheckIfEntityExists<T>(string name) where T : BaseEntity;
    }
}
