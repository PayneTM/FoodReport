using System.ComponentModel.DataAnnotations;
using FoodReport.DAL.Abstractions;

namespace FoodReport.DAL.Models
{
    public class Product : Entity
    {
        [Required] public string Name { get; set; }

        [Required] public string Provider { get; set; }
    }
}