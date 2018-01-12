using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FoodReport.DAL.Abstractions;

namespace FoodReport.BLL.Models
{
    public class EditReportModel<T> : Entity
    {
        [Required] public List<T> List { get; set; }
    }
}