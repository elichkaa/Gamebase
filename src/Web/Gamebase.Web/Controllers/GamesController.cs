namespace Gamebase.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Services;
    using Gamebase.Models;
    using InputModels.AddDelete;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class GamesController : Controller
    {
        private readonly IGamesService gamesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public GamesController(
            IGamesService gamesService, 
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.gamesService = gamesService;
            this.userManager = userManager;
            this.environment = environment;
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

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            var addGameViewModel = new AddGameInputModel
            {
                GameModes = this.gamesService.GetGameModes()
            };
            return this.View(addGameViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(AddGameInputModel input)
        {
            input.GameModes = this.gamesService.GetGameModes();
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            try
            {
                this.gamesService.AddGame(input, user.Id, $"{this.environment.WebRootPath}/img/user-images/");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            this.TempData["Message"] = "Game added successfully.";
            return this.Redirect("/");
        }
    }
}
