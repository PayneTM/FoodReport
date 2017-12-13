using FoodReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodReport.Models.Admin
{
    public class CreateUserViewModel
    {
        public User User { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}
