using System;
using System.Linq;
using System.Threading.Tasks;
using FoodReport.BLL.Interfaces.PasswordHashing;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using FoodReport.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodReport.Controllers
{
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;


        public AdminController(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.Users().GetAll());
        }

        [HttpGet("users/create")]
        public async Task<IActionResult> CreateUser()
        {
            return View(new CreateUserViewModel {Roles = await _unitOfWork.Roles().GetAll()});
        }

        [HttpPost("users/create")]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = model.User.Email,
                    Password = _passwordHasher.HashPassword(model.User.Password),
                    Role = model.User.Role
                };
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

        [HttpGet("users/edit/{id}")]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _unitOfWork.Users().Get(id);
            if (user == null) return NotFound();
            var model = new EditUserViewModel {Id = user.Id, Email = user.Email, Role = user.Role};
            return View(model);
        }

        [HttpPost("users/edit/{id}")]
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

        [HttpPost("users/delete/{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var user = await _unitOfWork.Users().Get(id);
            if (user != null)
                try
                {
                    await _unitOfWork.Users().Remove(user.Id);
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
            var user = await _unitOfWork.Users().Get(id);
            if (user == null) return NotFound();
            var model = new ChangePasswordViewModel {Id = user.Id, Email = user.Email};
            return View(model);
        }

        [HttpPost("users/changepass/{id}")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _unitOfWork.Users().Get(model.Id);

                if (user != null)
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
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost("roles/create")]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
                try
                {
                    await _unitOfWork.Roles().Add(new Role {Name = name});
                    return RedirectToAction("Roles");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            return View(name);
        }

        [HttpPost("roles/delete")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _unitOfWork.Roles().Get(id);
            if (role.Name == "Admin" || role.Name == "User") return RedirectToAction("Roles");
            try
            {
                var users = await _unitOfWork.Users().GetAll();
                var list = users.Where(x => x.Role == role.Name);
                foreach (var item in list)
                {
                    item.Role = "User";
                    await _unitOfWork.Users().Update(item.Id, item);
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