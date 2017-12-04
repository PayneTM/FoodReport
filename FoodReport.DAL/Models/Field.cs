using FoodReport.DAL.Abstractions;
using System;

namespace FoodReport.DAL.Models
{
    public class Field
    {
        public string Id = Guid.NewGuid().ToString();
        public int Count { get; set; }
        public decimal Price { get; set; }
        public string Product { get; set; }
    }
}
