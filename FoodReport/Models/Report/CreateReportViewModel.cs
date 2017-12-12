using FoodReport.DAL.Abstractions;
using FoodReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodReport.Models.Report
{
    public class CreateReportViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Field Fields { get; set; }
    }
}
