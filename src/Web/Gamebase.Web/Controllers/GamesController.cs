namespace Gamebase.Web.Controllers
{
    using Data.Services;
    using Microsoft.AspNetCore.Mvc;

    public class GamesController : Controller
    {
        private readonly IGamesService gamesService;

        public GamesController(IGamesService gamesService)
        {
            this.gamesService = gamesService;
        }

        public IActionResult All(int id)
        {
            var games = this.gamesService.GetAll(id);
            return this.View(games);
        }

        public IActionResult Details(int id)
        {
            var game = this.gamesService.GetSingle(id);
            return this.View(game);
        }
    }
}
