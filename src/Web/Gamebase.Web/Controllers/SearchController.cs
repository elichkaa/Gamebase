namespace Gamebase.Web.Controllers
{
    using System.Linq;
    using Data.Services;
    using Data.Services.Contracts;
    using InputModels.Search;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Search;

    public class SearchController : Controller
    {
        private readonly IGamesService gamesService;
        private readonly IDeveloperService developerService;

        public SearchController(IGamesService gamesService, IDeveloperService developerService)
        {
            this.gamesService = gamesService;
            this.developerService = developerService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Game()
        {
            return this.View();
        }

        public IActionResult GameResults(SearchGameInputModel input)
        {
            var games = gamesService.GetGame(input);
            return this.View(games);
        }

        public IActionResult Developer()
        {
            return this.View();
        }

        public IActionResult DeveloperResults(SearchDeveloperInputModel input)
        {
            var games = this.developerService.GetDeveloperByName(input);
            return this.View(games);
        }
    }
}
