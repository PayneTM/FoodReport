using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using FoodReport.DAL.Repos;
using FoodReport.Models.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodReport.Controllers
{
    [Authorize]
    [Route("api/report/")]
    public class ReportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportController(IOptions<Settings> options)
        {
            _unitOfWork = new UnitOfWork(options);
        }

        [HttpGet("all")]
        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWork.Reports().GetAll();
            return View(result);
        }

        [HttpGet("details/{id}")]

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = new EditReportViewModel();
            try
            {
                item.Products = await _unitOfWork.Products().GetAll();
                item.Report = await _unitOfWork.Reports().Get(id);
            }
            catch
            {
                return NotFound();
            }
            return View(item);
        }

        [Route("create")]
        public async Task<IActionResult> Create()
        {
            var result = new CreateReportViewModel
            {
                Products = await _unitOfWork.Products().GetAll()
            };
            return View(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] List<Field> @field)
        {
            if (ModelState.IsValid)
            {
                var report = new Report
                {
                    Date = DateTime.Now,
                    List = @field,
                    Owner = User.Identity.Name,
                    Status = "Pending",
                    isEdited = false
                };
                await _unitOfWork.Reports().Add(report);
                return RedirectToAction(nameof(Index));
            }
            return View(@field);
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = new EditReportViewModel
            {
                Report = await _unitOfWork.Reports().Get(id),
                Products = await _unitOfWork.Products().GetAll()
            };
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit([FromBody] ChangeDataReportViewModel item)
        {
            var report = await _unitOfWork.Reports().Get(item.Id);
            if (item.Id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    report.isEdited = true;
                    report.EditedBy = User.Identity.Name;
                    report.LastEdited = DateTime.Now;
                    report.List = item.List;
                    report.Status = "Pending";
                    await _unitOfWork.Reports().Update(item.Id, report);
                }
                catch
                {
                    throw new Exception();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }

        [Route("delete/{id}")]
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

        [HttpPost("delete/{id}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _unitOfWork.Reports().Remove(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("approve/{id}")]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusViewModel item)
        {
            var report = await _unitOfWork.Reports().Get(item.Id);
            if (report == null)
            {
                return NotFound();
            }
            try
            {
                report.isEdited = true;
                report.Status = item.Status;
                report.LastEdited = DateTime.Now;
                report.EditedBy = item.AdminName;
                report.Message = item.Reason;
                await _unitOfWork.Reports().Update(item.Id, report);
                return Ok();
            }
            catch
            {
                return Unauthorized();
            }

        }
        [HttpGet("search/{criteria}/{value}")]
        public async Task<IActionResult> Search(string criteria, string value)
        {
            var report = await _unitOfWork.Reports().GetAll();
            IEnumerable<Report> result = new List<Report>();
            if (criteria == "owner")
            {
                result = report.Where(x => x.Owner == value);
                ViewData["Message"] = "Your result for name - " + value;
            }
            else if (criteria == "status")
            {
                result = report.Where(x => x.Status == value);
                ViewData["Message"] = "Your result for status - " + value;
            }
            else if (criteria == "date")
            {
                result = report.Where(x => x.Date.ToShortDateString() == value);
                ViewData["Message"] = "Your result for date - " + value;
            }
            return View(result);
        }
    }
}