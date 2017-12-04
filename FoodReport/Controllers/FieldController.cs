using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodReport.DAL.Models;
using FoodReport.Models;
using FoodReport.DAL.Interfaces;
using Microsoft.Extensions.Options;
using FoodReport.DAL.Repos;

namespace FoodReport.Controllers
{
    [Route("api/makereport/")]
    public class FieldController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private List<Field> _fieldList;

        public FieldController(IOptions<Settings> options)
        {
            _unitOfWork = new UnitOfWork(options);
            _fieldList = new List<Field>();
        }

        // GET: Field
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View(_fieldList);
        }

        // GET: Field/Details/5
        [Route("details/{id}")]

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @field = _fieldList.FirstOrDefault(m => m.Id == id);
            if (@field == null)
            {
                return NotFound();
            }

            return View(@field);
        }

        // GET: Field/Create
        [Route("create")]

        public IActionResult Create()
        {
            return View();
        }


        //TODO: API Report Controller


        // POST: Field/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] List<Field> @field)
        {
            if (ModelState.IsValid)
            {
                var report = new Report
                {
                    Date = DateTime.Now,
                    List = @field,
                    Owner = User.Identity.Name,
                    Status = "Pending"
                };
                await _unitOfWork.Reports().Add(report);
                return Ok();
            }
           // return View(@field);
           return Json(@field);
        }

        // GET: Field/Edit/5
        [Route("edit/{id}")]

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @field = _fieldList.FirstOrDefault(m => m.Id == id);
            if (@field == null)
            {
                return NotFound();
            }
            return View(@field);
        }

        // POST: Field/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Count,Price,Product,Id")] Field @field)
        {
            if (id != @field.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var item = _fieldList.FirstOrDefault(m => m.Id == id);
                    var index = _fieldList.IndexOf(item);
                    _fieldList[index] = @field;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FieldExists(@field.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@field);
        }

        // GET: Field/Delete/5
        [Route("delete/{id}")]

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @field = _fieldList.FirstOrDefault(m => m.Id == id);

            if (@field == null)
            {
                return NotFound();
            }

            return View(@field);
        }

        // POST: Field/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var @field = _fieldList.FirstOrDefault(m => m.Id == id);

            _fieldList.Remove(@field);
            return RedirectToAction(nameof(Index));
        }

        private bool FieldExists(string id)
        {
            return _fieldList.Any(e => e.Id == id);
        }
    }
}
