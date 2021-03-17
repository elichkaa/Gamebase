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

            if (games == null)
            {
               return this.Redirect("/Home/Error");
            }

            return this.View(games);
        }

        public IActionResult Details(int id)
        {
            var game = this.gamesService.GetSingle(id);

            if (game == null)
            {
                return this.Redirect("/Home/Error");
            }

            return this.View(game);
        }
    }
}
