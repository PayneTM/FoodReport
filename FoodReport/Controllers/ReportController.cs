using FoodReport.BLL.Interfaces;
using FoodReport.BLL.Services;
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
    [Route("")]
    [Route("api/report/")]
    public class ReportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISearchService _searchService;
        private readonly IStatusReportService _statusReportService;

        public ReportController(IUnitOfWork unitOfWork, ISearchService searchService, IStatusReportService statusReportService)
        {
            _unitOfWork = unitOfWork;
            _searchService = searchService;
            _statusReportService = statusReportService;

        }

        [HttpGet("all")]
        [Route("")]
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
        public async Task<IActionResult> Create([FromBody] List<Field> field)
        {
            if (ModelState.IsValid)
            {
                await _statusReportService.onCreateStatus(field, User.Identity.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(field);
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
            if (item == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _statusReportService.onEditStatus(item, User.Identity.Name);
                }
                catch
                {
                    throw new Exception();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Edit));
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

        [Authorize(Roles = "Admin")]
        [HttpPost("approve/{id}")]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusViewModel item)
        {
            if (item == null) throw new NullReferenceException();
            try
            {
                await _statusReportService.onApproveStatus(item, User.Identity.Name);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search(string criteria, string value)
        {
            var result = await _searchService.Report().Search(criteria, value);
            ViewData["Message"] = result.Message;
            return View(result.List);
        }
        [HttpGet("refresh")]
        public IActionResult Refresh()
        {
            return RedirectToAction("Index", "Report");
        }

    }
}