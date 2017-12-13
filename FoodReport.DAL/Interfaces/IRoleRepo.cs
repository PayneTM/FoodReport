using FoodReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodReport.DAL.Interfaces
{
    public interface IRoleRepo : IRepository<Role>
    {
        Task<Role> FindRoleByName(string name); 
    }
}
