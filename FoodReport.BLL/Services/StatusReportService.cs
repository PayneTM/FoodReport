using FoodReport.BLL.Models;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using FoodReport.DAL.Repos;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodReport.BLL.Services
{
    public class StatusReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StatusReportService(IOptions<Settings> options)
        {
            _unitOfWork = new UnitOfWork(options);
        }
        public async Task CreateReportStatusPending(List<Field> field, string owner)
        {
            var report = new Report
            {
                Date = DateTime.Now,
                List = field,
                Owner = owner,
                Status = "Pending",
                isEdited = false
            };
            await _unitOfWork.Reports().Add(report);
        }

        public async Task EditReportStatusEdited(EditReportModel item, string owner)
        {
            var report = await _unitOfWork.Reports().Get(item.Id);
            if (report == null) throw new NullReferenceException();
            try
            {
                report.isEdited = true;
                report.EditedBy = owner;
                report.LastEdited = DateTime.Now;
                report.List = item.List;
                report.Status = "Pending";
                await _unitOfWork.Reports().Update(item.Id, report);
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
