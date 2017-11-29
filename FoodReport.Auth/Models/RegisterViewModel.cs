using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodReport.Auth.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(32, MinimumLength = 5, ErrorMessage = "The lenght of the password must be between 5 and 32 characters")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Wrong password confirmation")]
        public string ConfirmPassword { get; set; }

    }
}
