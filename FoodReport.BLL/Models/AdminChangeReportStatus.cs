using System.ComponentModel.DataAnnotations;
using FoodReport.DAL.Abstractions;

namespace FoodReport.BLL.Models
{
    public class AdminChangeReportStatus : Entity
    {
        [Required] public string Status { get; set; }

        public string Reason { get; set; }

        [Required] public string AdminName { get; set; }
    }
}