using FoodReport.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodReport.Models.Report
{
    public class CreateReportViewModel : Entity
    {
        [Required]
        public uint Count { get; set; }
        [StringLength(100,  ErrorMessage ="Too long description")]
        public string Description { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required]
        public string Product { get; set; }
        public string Owner { get; set; }
    }
}
