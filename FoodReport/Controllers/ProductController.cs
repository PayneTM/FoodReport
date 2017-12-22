using FoodReport.BLL.Interfaces.Search;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodReport.Controllers
{
    [Authorize]
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISearchService _searchService;

        public ProductController(IUnitOfWork unitOfWork, ISearchService searchService)
        {
            _unitOfWork = unitOfWork;
            _searchService = searchService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await GetProductInternal();
                return View(result);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }

        private async Task<IEnumerable<Product>> GetProductInternal()
        {
            return await _unitOfWork.Products().GetAll();
        }

        [Route("create")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Product item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _unitOfWork.Products().GetAll() is List<Product> products && products.Exists(x => x.Name == item.Name && x.Provider == item.Provider))
                    {
                        return RedirectToAction(nameof(Create));
                    }
                    await _unitOfWork.Products().Add(item);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [Route("edit/{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Edit(string id)

        {
            if (id == null)
            {
                return NotFound();
            }

            var toEdit = await _unitOfWork.Products().Get(id);
            if (toEdit == null)
            {
                return NotFound();
            }
            return View(toEdit);
        }

        [HttpPost("edit/{id}")]
        [Authorize(Roles = "Admin")]

        public IActionResult Edit(Product item)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Products().Update(item.Id, item);
            }
            return RedirectToAction(nameof(Index));
        }

        [Route("delete")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDelete = await _unitOfWork.Products().Get(id);
            if (toDelete == null)
            {
                return NotFound();
            }

            return View(toDelete);
        }

        // POST: TodoListElements/Delete/5
        [HttpPost("delete"), ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _unitOfWork.Products().Remove(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("prodsjson")]
        public async Task<JsonResult> GetProdsJson()
        {
            return Json(await _unitOfWork.Products().GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Search(string criteria, string value)
        {
            try
            {
                var result = await _searchService.Product().Search(criteria, value);
                ViewData["Message"] = result.Message;
                return View(result.List);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View();
            }
        }
    }
}
