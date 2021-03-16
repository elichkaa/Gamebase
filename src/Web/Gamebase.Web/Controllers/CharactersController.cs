namespace Gamebase.Web.Controllers
{
    using Data.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class CharactersController : Controller
    {
        private readonly ICharacterService characterService;

        public CharactersController(ICharacterService characterService)
        {
            this.characterService = characterService;
        }

        public IActionResult All(int id)
        {
            if (id < 0 || id > this.characterService.GetMaxPages())
            {
                return this.NotFound();
            }

            var characters = this.characterService.GetAll(id);
            return this.View(characters);
        }
    }
}
