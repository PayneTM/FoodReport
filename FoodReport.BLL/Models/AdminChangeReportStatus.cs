using FoodReport.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodReport.BLL.Models
{
    public class AdminChangeReportStatus: Entity
    {
        public string Status { get; set; }
        public string Reason { get; set; }
        public string AdminName { get; set; }
    }
}
