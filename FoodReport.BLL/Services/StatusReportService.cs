using FoodReport.BLL.Interfaces.Status;
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
    public class StatusReportService : IStatusReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StatusReportService(IOptions<Settings> options)
        {
            _unitOfWork = new UnitOfWork(options);
        }
        public async Task onCreateStatus(List<Field> field, string owner)
        {
            var report = new Report
            {
                Date = DateTime.Now,
                List = field,
                Owner = owner,
                Status = "Pending",
                isEdited = false
            };
            try
            {
                await _unitOfWork.Reports().Add(report);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task onEditStatus(EditReportModel<Field> item, string owner)
        {
            try
            {
                var report = await _unitOfWork.Reports().Get(item.Id);
                if (report == null) throw new NullReferenceException("Item Not Found");

                report.isEdited = true;
                report.EditedBy = owner;
                report.LastEdited = DateTime.Now;
                report.List = item.List;
                report.Status = "Pending";
                await _unitOfWork.Reports().Update(item.Id, report);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task onApproveStatus(AdminChangeReportStatus item, string owner)
        {
            try
            {
                var report = await _unitOfWork.Reports().Get(item.Id);
                if (report == null) throw new NullReferenceException("Item not found");

                report.isEdited = true;
                report.Status = item.Status;
                report.LastEdited = DateTime.Now;
                report.EditedBy = item.AdminName;
                report.Message = item.Reason;
                await _unitOfWork.Reports().Update(item.Id, report);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
