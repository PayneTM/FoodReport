using FoodReport.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodReport.DAL.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public int Available { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public string Provider { get; set; }
    }
}
