using FoodReport.BLL.Interfaces;
using FoodReport.BLL.Models;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodReport.BLL.Services
{
    public class SearchReportService: ISearchReport
    {
        private readonly IUnitOfWork _unitOfWork;
        public SearchReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<SearchModel<Report>> Search(string criteria, string value)
        {
            var report = await _unitOfWork.Reports().GetAll();
            try
            {
                return await GetInernalReport(report, criteria, value);
            }
            catch
            {
                throw new Exception();
            };
        }
        private async Task<SearchModel<Report>> GetInernalReport(IEnumerable<Report> report, string criteria, string value)
        {
            var model = new SearchModel<Report>();
            switch (criteria)
            {
                case "owner":
                    model.List = report.Where(x => x.Owner == value);
                    model.Message = "Your result for name - " + value;
                    break;
                case "date":
                    model.List = report.Where(x => x.Date.ToShortDateString() == value);
                    model.Message = "Your result for date - " + value;
                    break;
                case "status":
                    model.List = report.Where(x => x.Status == value);
                    model.Message = "Your result for status - " + value;
                    break;
            }
            return model;
        }
    }
}
