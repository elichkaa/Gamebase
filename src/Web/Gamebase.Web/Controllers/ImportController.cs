namespace Gamebase.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Scraping;

    public class ImportController : Controller
    {
        private readonly ISeeder seeder;

        public ImportController(ISeeder seeder)
        {
            this.seeder = seeder;
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
