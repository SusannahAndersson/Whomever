using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Whomever.Data.Entities;
using Whomever.Models;

namespace Whomever.Controllers
{
    //[Route("api/[Controller]")]
    //[ApiController]
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

        //usage: enables applicationuser to be redirected to webshop page (homecontroller) if login succeeds
        //when user submit --> post back to server triggered from login btn in views/account/login form
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
                    return RedirectToAction("WebShop", "Home");
                }
            }
            ModelState.AddModelError("", "Wrong email or password, please try again");
            return View();
        }

        //usage: enables applicationuser to sign out from webshop
        //note: this option is only available if applicationuser already is signed in, views/shared/layout
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}