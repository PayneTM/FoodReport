using FoodReport.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodReport.Models.Admin
{
    public class ChangePasswordViewModel :Entity
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
