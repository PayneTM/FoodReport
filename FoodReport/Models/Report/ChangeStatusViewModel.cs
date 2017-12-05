using FoodReport.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodReport.Models.Report
{
    public class ChangeStatusViewModel : Entity
    {
        public string Status { get; set; }
        public string Reason { get; set; }
        public string AdminName { get; set; }
    }
}
