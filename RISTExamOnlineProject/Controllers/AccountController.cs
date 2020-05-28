using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RISTExamOnlineProject.Models.db;

namespace RISTExamOnlineProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly SPTODbContext _sptoDbContext;

        public AccountController(SPTODbContext context)
        {
            _sptoDbContext = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string OperatorID, string Password)
        {
            if (!string.IsNullOrEmpty(OperatorID) && string.IsNullOrEmpty(Password)) return RedirectToAction("Login");

            //Check the user name and password
            //Here can be implemented checking logic from the database
            ClaimsIdentity identity = null;
            var isAuthenticated = false;
            string authority = null;
            var active = false;
            var querylogin = _sptoDbContext.vewOperatorAll
                .Where(x => x.OperatorID == OperatorID && x.Password == Password)
                .Select(c => new {c.OperatorID, c.Active, c.Authority});


            if (querylogin.Any())
            {
                foreach (var item in querylogin)
                {
                    authority = item.Authority;
                    active = item.Active;
                }

                if (active == false) return RedirectToAction("Login");

                if (authority == "9")
                {
                    //Create the identity for the Admin
                    identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, OperatorID),
                        new Claim(ClaimTypes.Role, "Admin")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    isAuthenticated = true;
                }

                if (authority == "0")
                {
                    //Create the identity for the user
                    identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, OperatorID),
                        new Claim(ClaimTypes.Role, "User")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    isAuthenticated = true;
                }
            }

           

            if (isAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}