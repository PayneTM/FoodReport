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
    public class SummaryReportService : ISummaryReport<SummaryModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public SummaryReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task ByDate(int date)
        {
            throw new NotImplementedException();
        }

        public async Task<SummaryModel> ByMonth(int month)
        {
            if (month < 1 || month > 12)
            {
                throw new Exception("Wrong month!");
            }
            var reports = await _unitOfWork.Reports().GetAll();
            var list = reports.Where(x => x.Date.Month == month && x.Status == "Approved");
            var result = new SummaryModel();
            //for each report in list of approved reports
            foreach (var report in list)
            {
                //for each field in reports
                foreach (var item in report.List)
                {
                    if (result.List == null)
                    {
                        result.List = new List<Field>();
                        result.List.Add(item);
                        result.TotalCount++;
                        result.TotalSum += item.Price;
                    }
                    else if (result.List != null)
                    {
                        //if false, add to summary report this field without changes
                        if (!result.List.Exists(x => x.Product == item.Product))
                        {
                            result.List.Add(item);
                            result.TotalCount++;
                            result.TotalSum += item.Price;
                        }
                        //otherwise 
                        else if (result.List.Contains(result.List.FirstOrDefault(x => x.Product == item.Product)))
                        {
                            var newfield = result.List.FirstOrDefault(x => x.Product == item.Product);
                            newfield.Price += item.Price;
                            newfield.Count += item.Count;
                            var index = result.List.FindIndex(x => x.Product == item.Product);
                            result.List[index] = newfield;
                            result.TotalSum += item.Price;
                        }
                    }
                }
            }
            return result;
        }

        public Task ByYear(int year)
        {
            throw new NotImplementedException();
        }
    }
}