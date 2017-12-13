using FoodReport.BLL.Interfaces;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using FoodReport.DAL.Repos;
using FoodReport.Models.Account;
using FoodReport.Models.Admin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
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
        private readonly IPasswordHasher _passwordHasher;


        public AdminController(IUnitOfWork unitOfWork, ISearchService searchService, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _searchService = searchService;
            _passwordHasher = passwordHasher;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index() => View(_unitOfWork.Users().GetAll());

        [Authorize(Roles = "Admin")]
        public IActionResult CreateUser() => View();

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(User model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, Password = _passwordHasher.HashPassword(model.Password) };
                try
                {
                    await _unitOfWork.Users().Add(user);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _unitOfWork.Users().Get(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new EditUserViewModel { Id = user.Id, Email = user.Email, Role = user.Role };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _unitOfWork.Users().Get(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;

                    try
                    {
                        await _unitOfWork.Users().Update(user.Id, user);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await _unitOfWork.Users().Get(id);
            if (user != null)
            {
                var result = await _unitOfWork.Users().Remove(user.Id);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await _unitOfWork.Users().Get(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _unitOfWork.Users().Get(model.Id);

                if (user != null)
                {
                    try
                    {
                        user.Password = _passwordHasher.HashPassword(model.NewPassword);
                        await _unitOfWork.Users().Update(user.Id, user);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User not found");
            }
            return View(model);
        }

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
