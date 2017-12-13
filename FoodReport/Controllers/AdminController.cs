using FoodReport.BLL.Interfaces;
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
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISearchService _searchService;

        public AdminController(IUnitOfWork unitOfWork, ISearchService searchService)
        {
            _unitOfWork = unitOfWork;
            _searchService = searchService;
        }

        //[Authorize(Roles = "admin")]
        //public IActionResult Index() => View(_unitOfWork.Users().GetAll());

        //[Authorize(Roles = "admin")]
        //public IActionResult CreateUser() => View();

        //[Authorize(Roles = "admin")]
        //[HttpPost]
        //public async Task<IActionResult> CreateUser(User model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = new User { Email = model.Email, Password = model.Password.GetHashCode};
        //        user.EmailConfirmed = true;
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }
        //        }
        //    }
        //    return View(model);
        //}

        //[Authorize(Roles = "admin")]
        //public async Task<IActionResult> EditUser(string id)
        //{
        //    var user = await _userManager.FindByIdAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    var model = new EditUserViewModel { Id = user.Id, Email = user.Email};
        //    return View(model);
        //}

        //[Authorize(Roles = "admin")]
        //[HttpPost]
        //public async Task<IActionResult> EditUser(EditUserViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByIdAsync(model.Id);
        //        if (user != null)
        //        {
        //            user.Email = model.Email;
        //            user.UserName = model.Email;

        //            var result = await _userManager.UpdateAsync(user);
        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                foreach (var error in result.Errors)
        //                {
        //                    ModelState.AddModelError(string.Empty, error.Description);
        //                }
        //            }
        //        }
        //    }
        //    return View(model);
        //}

        //[Authorize(Roles = "admin")]
        //[HttpPost]
        //public async Task<ActionResult> DeleteUser(string id)
        //{
        //    var user = await _userManager.FindByIdAsync(id);
        //    if (user != null)
        //    {
        //        var result = await _userManager.DeleteAsync(user);
        //    }
        //    return RedirectToAction("Index");
        //}

        //public async Task<IActionResult> ChangePassword(string id)
        //{
        //    var user = await _userManager.FindByIdAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    var model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
        //    return View(model);
        //}

        //[Authorize(Roles = "admin")]
        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByIdAsync(model.Id);
        //        if (user != null)
        //        {
        //            var _passwordValidator =
        //                HttpContext.RequestServices.GetService(typeof(IPasswordValidator<ApplicationUser>)) as IPasswordValidator<ApplicationUser>;
        //            var _passwordHasher =
        //                HttpContext.RequestServices.GetService(typeof(IPasswordHasher<ApplicationUser>)) as IPasswordHasher<ApplicationUser>;

        //            var result =
        //                await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
        //            if (result.Succeeded)
        //            {
        //                user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
        //                await _userManager.UpdateAsync(user);
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                foreach (var error in result.Errors)
        //                {
        //                    ModelState.AddModelError(string.Empty, error.Description);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Пользователь не найден");
        //        }
        //    }
        //    return View(model);
        //}

        //public IActionResult Roles()
        //{
        //    return View(_roleManager.Roles.ToList());
        //}

        //public IActionResult CreateRole() => View();

        //[HttpPost]
        //public async Task<IActionResult> CreateRole(string name)
        //{
        //    if (!string.IsNullOrEmpty(name))
        //    {
        //        var result = await _roleManager.CreateAsync(new IdentityRole(name));
        //        if (result.Succeeded)
        //        {
        //            return RedirectToAction("Roles");
        //        }
        //        else
        //        {
        //            foreach (var error in result.Errors)
        //            {
        //                ModelState.AddModelError(string.Empty, error.Description);
        //            }
        //        }
        //    }
        //    return View(name);
        //}

        //[HttpPost]
        //public async Task<IActionResult> DeleteRole(string id)
        //{
        //    var role = await _roleManager.FindByIdAsync(id);
        //    if (role != null)
        //    {
        //        var result = await _roleManager.DeleteAsync(role);
        //    }
        //    return RedirectToAction("Roles");
        //}

        //public IActionResult UserRoles() => View(_userManager.Users.ToList());

        //public async Task<IActionResult> EditUserRole(string userId)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user != null)
        //    {
        //        var userRoles = await _userManager.GetRolesAsync(user);
        //        var allRoles = _roleManager.Roles.ToList();
        //        var model = new ChangeRoleViewModel
        //        {
        //            UserId = user.Id,
        //            UserEmail = user.Email,
        //            UserRoles = userRoles,
        //            AllRoles = allRoles
        //        };
        //        return View(model);
        //    }

        //    return NotFound();
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditUserRole(string userId, List<string> roles)
        //{
        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user != null)
        //    {
        //        var userRoles = await _userManager.GetRolesAsync(user);
        //        var allRoles = _roleManager.Roles.ToList();
        //        var addedRoles = roles.Except(userRoles);
        //        var removedRoles = userRoles.Except(roles);
        //        await _userManager.AddToRolesAsync(user, addedRoles);
        //        await _userManager.RemoveFromRolesAsync(user, removedRoles);
        //        return RedirectToAction("UserRoles");
        //    }

        //    return NotFound();
        //}
    }
}
