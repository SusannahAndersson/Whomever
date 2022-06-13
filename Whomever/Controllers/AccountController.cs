using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Whomever.Data.Entities;
using Whomever.Models;

namespace Whomever.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(ILogger<AccountController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
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
                    return RedirectToAction("Webshop", "Home");
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

        //usage: create jwt token for signed and valid applicationuser (login and create new order)
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(300)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            //only create token if model and login is successful=valid
            if (ModelState.IsValid)
            {
                //find applicationuser by username(email) from model
                var applicationUser = await _userManager.FindByNameAsync(model.UserName);
                //if user is found then check password (not using cookies) to validate if match
                if (applicationUser != null)
                {
                    var checkUser = await _signInManager.CheckPasswordSignInAsync(applicationUser, model.Password, false);
                    //if password matches username then create token
                    if (checkUser.Succeeded)
                    {
                        //with claims
                        var claims = new[]
                        {
                            //sub subject since username=email
                            new Claim(JwtRegisteredClaimNames.Sub,
                            applicationUser.Email),
                            //imp pro
                            //new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                            //unique string f token
                            new Claim(JwtRegisteredClaimNames.Jti,
                            Guid.NewGuid().ToString()),
                            //unique name f e obj mapped controller-view
                            new Claim(JwtRegisteredClaimNames.UniqueName,
                            applicationUser.UserName)};

                        //need key to sign validate token
                        var key = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]
                            ));
                        var creds = new SigningCredentials(
                            key, SecurityAlgorithms.HmacSha384
                            );
                        var token = new JwtSecurityToken(
                          _configuration["Tokens:Issuer"],
                          _configuration["Tokens:Audience"],
                          claims,
                          expires: DateTime.UtcNow.AddHours(1),
                          signingCredentials: creds);

                        var auth = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Created("", auth);
                    }
                }
            }
            return BadRequest();
        }

        //returns register/signup view for new users
        public IActionResult Register()
        {
            return View();
        }

        //usage: enables new user to register an account and saves new user to db
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var regUser = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var pass = await _userManager.CreateAsync(
                    regUser, model.Password);

                if (pass.Succeeded)
                {
                    await _signInManager.SignInAsync(regUser, isPersistent: false);
                    return RedirectToAction("Webshop", "Home");
                }

                //displays each error for each prop
                foreach (var error in pass.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError("", $"Unable to register new user {regUser}");
            }
            return View(model);
        }
    }
}