using FoodReport.DAL.Abstractions;
using FoodReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FoodReport.BLL.Models
{
    public class EditReportModel<T> : Entity
    {
        [Required]
        public List<T> List { get; set; }
    }
}
