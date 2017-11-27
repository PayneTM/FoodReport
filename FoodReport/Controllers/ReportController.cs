using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using FoodReport.DAL.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace FoodReport.Controllers
{
    public class ReportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportController(IOptions<Settings> options)
        {
            _unitOfWork = new UnitOfWork(options);
        }

        // GET: Report
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.Reports().GetAll());
        }

        // GET: Report/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _unitOfWork.Reports().Get(id) ?? new Report();
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Report/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Report/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Count,Description,TotalPrice,Date,Owner,Id")] Report report)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Reports().Add(report);
                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }

        // GET: Report/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _unitOfWork.Reports().Get(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // POST: Report/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Count,Description,TotalPrice,Date,Owner,Id")] Report report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.Reports().Update(id,report);
                }
                catch //(DbUpdateConcurrencyException)
                {
                    //if (!ReportExists(report.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }

        // GET: Report/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _unitOfWork.Reports().Get(id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Report/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _unitOfWork.Reports().Remove(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool ReportExists(string id)
        //{
        //    return _context.Report.Any(e => e.Id == id);
        //}
    }
}
