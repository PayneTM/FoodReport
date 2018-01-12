using System.Collections.Generic;
using FoodReport.DAL.Models;

namespace FoodReport.Models.Admin
{
    public class CreateUserViewModel
    {
        public User User { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}