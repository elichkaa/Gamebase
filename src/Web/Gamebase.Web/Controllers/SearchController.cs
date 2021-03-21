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
        private readonly ICharacterService characterService;

        public SearchController(IGamesService gamesService, IDeveloperService developerService, ICharacterService characterService)
        {
            this.gamesService = gamesService;
            this.developerService = developerService;
            this.characterService = characterService;
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
            if (!ModelState.IsValid)
            {
                this.ModelState.AddModelError(string.Empty, "Game name should be minimum 2 symbols.");
                this.TempData["ModelState"] = this.ModelState.Root.Errors[0].ErrorMessage;
                return this.Redirect("/Search/Game/");
            }
            var games = gamesService.SearchGames(input);
            return this.View(games);
        }

        public IActionResult Developer()
        {
            return this.View();
        }

        public IActionResult DeveloperResults(SearchDeveloperInputModel input)
        {
            if (!ModelState.IsValid)
            {
                this.ModelState.AddModelError(string.Empty, "Developer name should be minimum 2 symbols.");
                this.TempData["ModelState"] = this.ModelState.Root.Errors[0].ErrorMessage;
                return this.Redirect("/Search/Developer/");
            }
            var developers = this.developerService.GetDeveloperByName(input);
            return this.View(developers);
        }

        public IActionResult Character()
        {
            return this.View();
        }

        public IActionResult CharacterResults(SearchCharacterInputModel input)
        {
            if (!ModelState.IsValid)
            {
                this.ModelState.AddModelError(string.Empty, "Character name should be minimum 2 symbols.");
                this.TempData["ModelState"] = this.ModelState.Root.Errors[0].ErrorMessage;
                return this.Redirect("/Search/Character/");
            }
            var characters = this.characterService.GetCharacterByName(input);
            return this.View(characters);
        }
    }
}
