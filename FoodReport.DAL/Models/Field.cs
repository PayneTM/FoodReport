using FoodReport.DAL.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace FoodReport.DAL.Models
{
    public class Field
    {
        public string Id = Guid.NewGuid().ToString();
        [RegularExpression(@"\d+(\.|,)?\d*", ErrorMessage = "Wrong data!")]
        public int Count { get; set; }
        [RegularExpression(@"\d+(\.|,)?\d*", ErrorMessage = "Wrong data!")]
        public decimal Price { get; set; }
        public string Product { get; set; }
    }
}
