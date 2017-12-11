using FoodReport.DAL.Abstractions;
using FoodReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodReport.Models.Report
{
    public class ChangeDataReportViewModel : Entity
    {
        public List<Field> List { get; set; }
    }
}
