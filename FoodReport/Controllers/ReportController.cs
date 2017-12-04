﻿using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using FoodReport.DAL.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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

        // GET: Field
        
        [HttpGet("all")]
        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWork.Reports().GetAll();
            //return View(Json(await _unitOfWork.Reports().GetAll());
            return View(result);
        }

        // GET: Field/Details/5
        [HttpGet("details/{id}")]

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

            //return Json(report);
            return View(report);
        }

        // GET: Field/Create
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Field/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("create")]
        //[ValidateAntiForgeryToken]
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
            //return Json(@field);
        }

        // GET: Field/Edit/5
        [HttpGet("edit/{id}")]
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
            //return Json(report);

            return View(report);
        }

        // POST: Field/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("edit/{id}")]
        //[ValidateAntiForgeryToken]


        //      expected: id, fields != null
        //      actual: id != null, fields == null
        // TODO: fix it!!
        public async Task<IActionResult> Edit(string id, [FromBody] List<Field> fields)
        {
            var report = await _unitOfWork.Reports().Get(id);
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    report.isEdited = true;
                    report.EditingTime = DateTime.Now;
                    report.List = fields;
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
            //return Json(report);
            return View(report);
        }

        // GET: Field/Delete/5
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

            //return Json(report);
            return View(report);
        }

        // POST: Field/Delete/5
        [HttpPost("delete/{id}"), ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _unitOfWork.Reports().Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
