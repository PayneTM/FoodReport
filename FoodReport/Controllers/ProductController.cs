using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodReport.DAL.Models;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Repos;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FoodReport.Controllers
{
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IOptions<Settings> options)
        {
            _unitOfWork = new UnitOfWork(options);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await GetProductInternal();
            return View(result);
        }

        private async Task<IEnumerable<Product>> GetProductInternal()
        {
            return await _unitOfWork.Products().GetAll();
        }

        // GET api/notes/5
        [Route("details")]
        public IActionResult Details() => View();

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            return View( await GetNoteByIdInternal(id));
        }

        private async Task<Product> GetNoteByIdInternal(string id)
        {
            return await _unitOfWork.Products().Get(id) ?? new Product();
        }
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }
        //POST api/notes
        [HttpPost("create")]
        public IActionResult Create([Bind("Name,Available,Unit,Price,Provider")]Product item)
        {
            _unitOfWork.Products().Add(item);
            return RedirectToAction(nameof(Index));
        }

        // PUT api/notes/5
        [Route("edit/{id}")]
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
        public IActionResult Edit([Bind("Id,Name,Available,Unit,Price,Provider")]Product item)
        {
            _unitOfWork.Products().Update(item.Id,item);
            return RedirectToAction(nameof(Index));
        }

        // DELETE api/notes/5
        //[Route("delete/{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    await _unitOfWork.Products().Remove(id);
        //    return RedirectToAction(nameof(Index));
        //}

        [Route("delete")]

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _unitOfWork.Products().Remove(id);
            //    return RedirectToAction(nameof(Index));
            return RedirectToAction(nameof(Index));
        }

    }
}
