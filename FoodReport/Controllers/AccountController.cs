using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FoodReport.BLL.Interfaces.PasswordHashing;
using FoodReport.BLL.Interfaces.UserManager;
using FoodReport.Common.Interfaces;
using FoodReport.DAL.Models;
using FoodReport.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodReport.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ICustomUserManager _userManager;

        public AccountController(IPasswordHasher passwordHasher, ICustomUserManager userManager, IMapper mapper)
        {
            _passwordHasher = passwordHasher;
            _userManager = userManager;
            _mapper = mapper;
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
                var usr = _mapper.Map<LoginViewModel, IUser>(item);
                try
                {
                    usr = await _userManager.PasswordValidate(usr);
                    if (usr != null)
                    {
                        await Authenticate(item.Email, usr.Role);

                        return RedirectToAction("Index", "Report");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    ModelState.AddModelError("", "Wrong username or password!");
                    throw;
                }
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
                    "User"
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
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}