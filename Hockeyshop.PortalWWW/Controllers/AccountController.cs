using BCrypt.Net;
using Hockeyshop.Data.Data;
using Hockeyshop.Data.Data.Core;
using Hockeyshop.PortalWWW.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using System.Security.Claims;

namespace Hockeyshop.PortalWWW.Controllers
{
    public class AccountController : BaseController
    {
        private readonly HockeyshopContext _context;

        public AccountController(HockeyshopContext context)
        {
            _context = context;
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_context.Users.Any(u => u.Login == model.Login))
            {
                ModelState.AddModelError("Login", "This login is already taken.");
                return View(model);
            }

            var user = new User
            {
                Login = model.Login,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                PostCode = "00000",
                City = "City...",
                StreetAndNumber = "Street 1",
                IdUserRole = 1 //1 = zwykły klient
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await SignInUser(user); //automatycznie loguje po rejestracji

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users.FirstOrDefault(u => u.Login == model.Login);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError("", "Invalid login or password.");
                return View(model);
            }

            await SignInUser(user);

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        //logowanie helper
        private async Task SignInUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim("FullName", $"{user.FirstName} {user.LastName}")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        }
    }
}
