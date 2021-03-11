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

        public IActionResult All()
        {
            var games = this.gamesService.GetAll();
            return this.View(games);
        }

        public IActionResult Details()
        {
            return this.View();
        }
    }
}
