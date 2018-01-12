using System.Collections.Generic;
using FoodReport.DAL.Abstractions;
using FoodReport.DAL.Models;

namespace FoodReport.Models.Report
{
    public class EditReportViewModel : Entity
    {
        public DAL.Models.Report Report { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}