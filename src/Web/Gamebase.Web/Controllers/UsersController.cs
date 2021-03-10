namespace Gamebase.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Gamebase.Models;
    using InputModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
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
                await this.signInManager.PasswordSignInAsync(input.Username, input.Password, input.RememberMe, lockoutOnFailure: false);
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
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            this.TempData["Message"] = "You logged out successfully.";
            return this.Redirect("/");
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
            if (result.Succeeded)
            {
                await this.signInManager.SignInAsync(user, isPersistent: false);
                this.TempData["Message"] = "You registered successfully.";
                return this.Redirect("/");
            }
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, error.Description);
            }
            return this.View();
        }
    }
}
