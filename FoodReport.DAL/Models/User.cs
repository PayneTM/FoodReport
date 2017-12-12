using FoodReport.DAL.Abstractions;

namespace FoodReport.DAL.Models
{
    public class User : Entity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}