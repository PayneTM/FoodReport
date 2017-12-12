using FoodReport.DAL.Abstractions;
using FoodReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodReport.Models.Report
{
    public class EditReportViewModel : Entity
    {
        public FoodReport.DAL.Models.Report Report { get; set; }
        public IEnumerable<Product> Products { get; set; }   
    }
}
