namespace Gamebase.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class HomePageViewModel
    {
        public ICollection<GameOnHomePageViewModel> RandomGames { get; set; }
        public ICollection<GameOnHomePageViewModel> RecentGames { get; set; }
    }
}
