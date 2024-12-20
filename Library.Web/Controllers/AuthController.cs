using Microsoft.AspNetCore.Mvc;
using Library.Web.Models;
using Library.Domain.AuthTransactionScripts;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Library.Web.Controllers
{
    public class AuthController : Controller
    {
        // GET: AuthController/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: AuthController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(AuthModel model)
        {
            if (ModelState.IsValid)
            {
                var script = new LoginTransactionScript();
                script.Username = model.Username;
                script.Password = model.Password;
                script.Execute();
                if (script.Output == true)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, script.Staff.StaffID.ToString()),
                        new Claim(ClaimTypes.Name, script.Staff.FirstName + " " + script.Staff.LastName)
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties);

                    TempData["Message"] = "Přihlášení proběhlo úspěšně";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("Index", "Home");

                } else
                {
                    ViewBag.Message = "Špatné uživatelské jméno nebo heslo";
                    ViewBag.MessageType = "danger";
                    return View();
                }
            }
            return View();

        }

        // GET: AuthController/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Message"] = "Odhlášení proběhlo úspěšně";
            TempData["MessageType"] = "success";
            return RedirectToAction("Login");
        }
    }
}
