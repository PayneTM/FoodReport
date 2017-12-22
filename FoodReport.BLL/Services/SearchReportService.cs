using FoodReport.BLL.Interfaces.Search;
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
    public class SearchReportService : ISearchReport
    {
        private readonly IUnitOfWork _unitOfWork;
        public SearchReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<SearchModel<Report>> Search(string criteria, string value)
        {
            try
            {
                var report = await _unitOfWork.Reports().GetAll();

                return await GetInernalReport(report, criteria, value);
            }
            catch (Exception ex)
            {
                throw ex;
            };
        }
        private async Task<SearchModel<Report>> GetInernalReport(IEnumerable<Report> report, string criteria, string value)
        {
            var model = new SearchModel<Report>();
            switch (criteria.ToLower())
            {
                case "owner":
                    model.List = report.Where(x => x.Owner.ToLower().Contains(value.ToLower()));
                    model.Message = "Your result for name - " + value;
                    break;
                case "date":
                    model.List = report.Where(x => x.Date.ToShortDateString().Contains(value.ToLower()));
                    model.Message = "Your result for date - " + value;
                    break;
                case "status":
                    model.List = report.Where(x => x.Status.ToLower().Contains(value.ToLower()));
                    model.Message = "Your result for status - " + value;
                    break;
                default: throw new Exception(criteria + " - wrong criteria");
            }
            if (model.List.Count() == 0) throw new Exception("Nothing found on - " + value);
            return model;
        }
    }
}
