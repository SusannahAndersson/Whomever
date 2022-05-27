using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Whomever.Data.Entities;
using Whomever.Models;

namespace Whomever.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(ILogger<AccountController> logger, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            //user signed in
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        //post back to server from sumbit in views login form
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(
                    model.UserName,
                  model.Password,
                  model.RememberMe,
                  false);

                if (loginResult.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }
                    else
                    {
                        RedirectToAction("WebShop", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Failed to login");

            return View();
        }
    }
}