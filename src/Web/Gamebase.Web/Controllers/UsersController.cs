namespace Gamebase.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Services;
    using Gamebase.Models;
    using InputModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.Games;
    using ViewModels.Users;

    public class UsersController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IGamesService gamesService;

        public UsersController(
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager,
            IGamesService gamesService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.gamesService = gamesService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var login = await this.signInManager.PasswordSignInAsync(input.Username, input.Password, input.RememberMe, lockoutOnFailure: false);
                if (!login.Succeeded)
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return this.View();
                }
                this.TempData["Message"] = "You logged in successfully.";

            }
            else
            {
                this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return this.View();
            }

            await this.userManager.GetUserAsync(this.User);

            return this.Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            this.TempData["Message"] = "You logged out successfully.";
            return this.Redirect("/");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterInputModel input)
        {
            if (!this.ModelState.IsValid) return this.View();
            var user = new ApplicationUser
            {
                UserName = input.Username,
                Email = input.Email,
                CreatedOn = DateTime.UtcNow
            };
            var result = await this.userManager.CreateAsync(user, input.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError(string.Empty, error.Description);
                }
                return this.View();
            }
            await this.signInManager.SignInAsync(user, isPersistent: false);
            this.TempData["Message"] = "You registered successfully.";
            return this.Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> Account()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var userVm = new UserAccountViewModel()
            {
               Username = currentUser.UserName,
               GamesByUser = this.gamesService.GetGamesByUser(currentUser.Id).ToList()
            };
            return this.View(userVm);
        }
    }
}
