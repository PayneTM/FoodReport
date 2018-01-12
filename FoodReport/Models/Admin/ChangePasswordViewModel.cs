using FoodReport.DAL.Abstractions;

namespace FoodReport.Models.Admin
{
    public class ChangePasswordViewModel : Entity
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}