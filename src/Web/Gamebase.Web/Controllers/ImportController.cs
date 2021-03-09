namespace Gamebase.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ImportController : Controller
    {

        public ImportController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Seed()
        {
            this.TempData["Message"] = "Data imported successfully.";
            return this.Redirect("/");
        }
    }
}
