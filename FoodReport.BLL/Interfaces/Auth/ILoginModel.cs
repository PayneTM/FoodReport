using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FoodReport.BLL.Interfaces.Auth
{
    public interface ILoginModel
    {
        [Required]
        [EmailAddress]
        string Email { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 5)]
        string Password { get; set; }
    }
}
