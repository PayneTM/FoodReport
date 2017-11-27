using FoodReport.DAL.Abstractions;
using System;

namespace FoodReport.DAL.Models
{
    public class Report : Entity
    {
        public int Count { get; set; }
        public string Description { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }
        public virtual Product Product { get; set; }
        public string Owner { get; set; }
    }
}
