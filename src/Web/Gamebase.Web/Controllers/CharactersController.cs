namespace Gamebase.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CharactersController : Controller
    {
        public CharactersController()
        {
            
        }

        public IActionResult All(int id)
        {
            //if (id < 0 || id > .GetMaxPages())
            //{
            //    return this.NotFound();
            //}
            return this.View();
        }
    }
}
