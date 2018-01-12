using System.Collections.Generic;
using FoodReport.DAL.Models;

namespace FoodReport.Models.Report
{
    public class CreateReportViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Field Fields { get; set; }
    }
}