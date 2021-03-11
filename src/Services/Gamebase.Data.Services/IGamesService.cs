namespace Gamebase.Data.Services
{
    using System.Collections.Generic;
    using Web.ViewModels.Games;

    public interface IGamesService
    {
        public List<GameOnAllPageViewModel> GetAll();
        public GameOnDetailsPageViewModel GetSingle(int id);
    }
}
