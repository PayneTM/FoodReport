using FoodReport.DAL.Abstractions;
using FoodReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodReport.BLL.Models
{
    public class EditReportModel : Entity
    {
        public List<Field> List { get; set; }
    }
}
