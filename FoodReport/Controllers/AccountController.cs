using FoodReport.BLL.Interfaces.PasswordHashing;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using FoodReport.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FoodReport.BLL.Interfaces.UserManager;

namespace FoodReport.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly ICustomUserManager _userManager;
        public AccountController(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IMapper mapper, ICustomUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _userManager = userManager;
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
            try
            {
                var usr = await _userManager.Create(
                    new User
                    {
                        Email = model.Email,
                        Password = _passwordHasher.HashPassword(model.Password)
                    },
                    role: "User"
                );
                await Authenticate(model.Email, usr.Role);

                return RedirectToAction("Index", "Report");
            }
            catch
            {
                ModelState.AddModelError("", "Wrong username or password!");
                return View(model);
            }
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