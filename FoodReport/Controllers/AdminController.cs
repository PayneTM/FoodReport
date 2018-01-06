using FoodReport.BLL.Interfaces.UserManager;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using FoodReport.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FoodReport.Controllers
{
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ICustomUserManager _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(ICustomUserManager userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index() => View(await _userManager.GetAllUsers());

        [HttpGet("users/create")]
        public async Task<IActionResult> CreateUser() => View(
            new CreateUserViewModel
            {
                Roles = await _unitOfWork.Roles().GetAll()
            });

        [HttpPost("users/create")]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userManager.Create(
                        new User
                        {
                            Email = model.User.Email,
                            Password = model.User.Password
                        },
                        role: model.User.Role);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(model);
                }
            }
            ModelState.AddModelError(string.Empty, "Wrong data!");
            return View(model);
        }
        [HttpGet("users/edit/{id}")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.GetById(id);
            if (user == null) return NotFound();
            var model = new EditUserViewModel { Id = user.Id, Email = user.Email, Role = user.Role };
            return View(model);
        }

        [HttpPost("users/edit/{id}")]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetById(model.Id);
                    if (user == null) return View(model);

                    user.Email = model.Email;
                    user.Role = model.Role;

                    await _userManager.Edit(user, user.Id);
                    return RedirectToAction("Index");

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        [HttpPost("users/delete/{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            try
            {
                await _userManager.Delete(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index");
        }
        [HttpGet("users/changepass/{id}")]
        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await _userManager.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost("users/changepass/{id}")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userManager.ChangePassword(model.Id, model.NewPassword);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User not found");
            }
            return View(model);
        }
        [HttpGet("roles")]
        public async Task<IActionResult> Roles()
        {
            return View(await _unitOfWork.Roles().GetAll());
        }
        [HttpGet("roles/create")]
        public IActionResult CreateRole() => View();

        [HttpPost("roles/create")]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    await _unitOfWork.Roles().Add(new Role { Name = name });
                    return RedirectToAction("Roles");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(name);
        }
        [HttpPost("roles/delete")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _unitOfWork.Roles().Get(id);
            if (role.Name == "Admin" || role.Name == "User")
            {
                return RedirectToAction("Roles");
            }
            try
            {
                var users = await _userManager.GetAllUsers();
                var list = users.Where(x => x.Role == role.Name);
                foreach (var item in list)
                {
                    await _userManager.ChangeRole(item.Id, role: "User");
                }
                await _unitOfWork.Roles().Remove(id);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return RedirectToAction("Roles");
            }
            return RedirectToAction("Roles");
        }
    }
}
