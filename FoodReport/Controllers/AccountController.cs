using FoodReport.BLL.Interfaces.PasswordHashing;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using FoodReport.DAL.Repos;
using FoodReport.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodReport.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        public AccountController(IOptions<Settings> options, IPasswordHasher passwordHasher)
        {
            _unitOfWork = new UnitOfWork(options);
            _passwordHasher = passwordHasher;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel item)
        {
            if (ModelState.IsValid)
            {
                User user = await _unitOfWork.Users().Get(item.Email, _passwordHasher.HashPassword(item.Password));
                if (user != null)
                {
                    await Authenticate(item.Email, user.Role);

                    return RedirectToAction("Index", "Report");
                }
                ModelState.AddModelError("", "Wrong username or password!");
            }
            return View(item);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _unitOfWork.Users().Get(model.Email, _passwordHasher.HashPassword(model.Password));
            if (user == null)
            {
                var role = await _unitOfWork.Roles().FindRoleByName("User");
                await _unitOfWork.Users().Add(new User { Email = model.Email, Password = _passwordHasher.HashPassword(model.Password), Role = role.Name});

                await Authenticate(model.Email,role.Name);

                return RedirectToAction("Index", "Report");
            }
            ModelState.AddModelError("", "Wrong username or password!");
            return View(model);
        }

        private async Task Authenticate(string userName, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}