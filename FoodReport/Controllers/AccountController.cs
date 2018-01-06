using AutoMapper;
using FoodReport.BLL.Interfaces.UserManager;
using FoodReport.DAL.Models;
using FoodReport.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodReport.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly ICustomUserManager _userManager;
        private readonly IMapper _mapper;
        public AccountController(ICustomUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel item)
        {
            if (ModelState.IsValid)
            {
                var usr = _mapper.Map<LoginViewModel, User>(item);
                try
                {
                    usr = await _userManager.PasswordValidate(usr);
                    if (usr != null)
                    {
                        await Authenticate(usr.Email, usr.Role);

                        return RedirectToAction("Index", "Report");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    ModelState.AddModelError("", "Wrong username or password!");
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
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            { 
                var usr = await _userManager.Create(
                    new User
                    {
                        Email = model.Email,
                        Password = model.Password
                    },
                    role: "User"
                );
                await Authenticate(usr.Email, usr.Role);

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