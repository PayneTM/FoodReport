using FoodReport.DAL.Abstractions;

namespace FoodReport.DAL.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Provider { get; set; }
    }
}
