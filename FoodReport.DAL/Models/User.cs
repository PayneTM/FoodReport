using FoodReport.DAL.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace FoodReport.DAL.Models
{
    public class User : Entity
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}