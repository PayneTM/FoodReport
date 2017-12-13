using FoodReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodReport.BLL.Models
{
    public class SummaryModel
    {
        public List<Field> List { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalSum { get; set; }
    }
}
