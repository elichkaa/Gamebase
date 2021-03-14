namespace Gamebase.Web.Controllers
{
    using System.Linq;
    using Data.Services;
    using InputModels.Search;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Search;

    public class SearchController : Controller
    {
        private readonly IGamesService gamesService;

        public SearchController(IGamesService gamesService)
        {
            this.gamesService = gamesService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Game(SearchGameInputModel input)
        {
            return this.View(input);
        }

        public IActionResult GameResults(string name, string developerName)
        {
            var games = gamesService.GetGameByName(name, developerName);
            return this.View(games);
        }
    }
}
