using FoodReport.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FoodReport.BLL.Models
{
    public class AdminChangeReportStatus: Entity
    {
        [Required]
        public string Status { get; set; }
        public string Reason { get; set; }
        [Required]
        public string AdminName { get; set; }
    }
}
