using FoodReport.DAL.Abstractions;
using System;

namespace FoodReport.DAL.Models
{
    public class Report : Entity
    {
        public uint Count { get; set; }
        public string Description { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public string Product { get; set; }
        public string Owner { get; set; }
    }
}
