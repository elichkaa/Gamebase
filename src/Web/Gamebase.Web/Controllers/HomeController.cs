using Gamebase.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Gamebase.Web.Controllers
{
    using Data.Services;
    using ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IGamesService gamesService;

        public HomeController(IGamesService gamesService)
        {
            this.gamesService = gamesService;
        }

        public IActionResult Index()
        {
            var hpViewModel = new HomePageViewModel
            {
                RandomGames = this.gamesService.GetFourRandomGames(),
                RecentGames = this.gamesService.GetThreeMostRecentGames()
            };
            return View(hpViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
