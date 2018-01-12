using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodReport.BLL.Interfaces;
using FoodReport.BLL.Models;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;

namespace FoodReport.BLL.Services
{
    public class SummaryReportService : ISummaryReport<SummaryModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SummaryReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<SummaryModel> CreateSummary(DateTime fromDate, DateTime toDate)
        {
            IEnumerable<Report> list;
            var wtf = fromDate.CompareTo(toDate);
            if (wtf < 0)
                list = await FindReports(fromDate, toDate);
            else if (wtf > 0)
                list = await FindReports(toDate, fromDate);
            else
                list = await FindReports(fromDate, fromDate);
            if (list == null) throw new NullReferenceException("There are no approved reports between these dates");

            var result = new SummaryModel();
            //for each report in list of approved reports
            foreach (var report in list)
                //for each field in reports
            foreach (var item in report.List)
                if (result.List == null)
                {
                    result.List = new List<Field> {item};
                    result.TotalCount++;
                    result.TotalSum += item.Price * item.Count;
                }
                else
                {
                    var isFieldExists = result.List.Exists(x => x.Product == item.Product);
                    if (isFieldExists)
                    {
                        var field = result.List.FirstOrDefault(x => x.Product == item.Product);
                        var index = result.List.FindIndex(x => x.Equals(field));

                        field.Price += item.Price * item.Count;
                        field.Count += item.Count;
                        result.List[index] = field;
                        result.TotalSum += item.Price * item.Count;
                    }
                    //if false, add to summary report this field without changes
                    else
                    {
                        result.List.Add(item);
                        result.TotalCount++;
                        result.TotalSum += item.Price * item.Count;
                    }
                }

            return result;
        }

        private async Task<IEnumerable<Report>> FindReports(DateTime from, DateTime to)
        {
            var reports = await _unitOfWork.Reports().GetAll();
            var list = reports.Where(x =>
                x.Date.CompareTo(from) >= 0 && x.Date.CompareTo(to) <= 0 && x.Status == "Approved");
            return list;
        }
    }
}