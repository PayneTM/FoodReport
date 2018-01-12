using System.Threading.Tasks;
using FoodReport.DAL.Models;

namespace FoodReport.DAL.Interfaces
{
    public interface IRoleRepo : IRepository<Role>
    {
        Task<Role> FindRoleByName(string name);
    }
}