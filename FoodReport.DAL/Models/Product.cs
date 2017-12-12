using FoodReport.DAL.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace FoodReport.DAL.Models
{
    public class Product : Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Provider { get; set; }
    }
}
