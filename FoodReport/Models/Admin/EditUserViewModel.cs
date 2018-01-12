using System.ComponentModel.DataAnnotations;
using FoodReport.DAL.Abstractions;

namespace FoodReport.Models.Admin
{
    public class EditUserViewModel : Entity
    {
        [Required] public string Email { get; set; }

        [Required] public string Role { get; set; }
    }
}