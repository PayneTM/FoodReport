using FoodReport.DAL.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace FoodReport.DAL.Models
{
    public class Field
    {
        public string Id = Guid.NewGuid().ToString();
        [RegularExpression(@"\d+(\.|,)?\d*", ErrorMessage = "Wrong data!")]
        [Required]
        public int Count { get; set; }
        [RegularExpression(@"\d+(\.|,)?\d*", ErrorMessage = "Wrong data!")]
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Product { get; set; }
    }
}
