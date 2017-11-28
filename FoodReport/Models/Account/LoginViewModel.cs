using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodReport.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set;}
        [Required]
        [StringLength(32, MinimumLength = 5)]
        public string Password { get; set; }
    }
}
