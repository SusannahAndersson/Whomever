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

        //usage: create jwt token for signed and valid applicationuser
        //[HttpPost("createtoken")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(300)]
        [ProducesResponseType(400)]
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
                        ////    await _userManager.CreateAsync(applicationUser);
                        var tokenClaim = new[]
                        {
              //sub subject since username=email
              new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Email),
              //imp pro
              //new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
              //unique name f e obj mapped controller-view
              new Claim(JwtRegisteredClaimNames.UniqueName, applicationUser.UserName),
              //unique string fe token
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
                        //need key to sign validate token
                        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var signCreds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha384);
                        var jwtToken = new JwtSecurityToken(_configuration["Tokens:Issuer"], _configuration["Tokens:Audience"], tokenClaim, expires: DateTime.UtcNow.AddHours(1), signingCredentials: signCreds);
                        var stringToken = new
                        {
                            jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                            expiration = jwtToken.ValidTo
                        };
                        return Created("", stringToken);
                    }
                }
            }
            return BadRequest();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var registerUser = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var registerResult = await _userManager.CreateAsync(registerUser, model.Password);

                if (registerResult.Succeeded)
                {
                    await _signInManager.SignInAsync(registerUser, isPersistent: false);
                    return RedirectToAction("Webshop", "Home");
                }

                foreach (var error in registerResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError("", "Invalid Register Attempt");
            }
            return View(model);
        }
    }
}