using System.Threading.Tasks;
using FoodReport.DAL.Models;

namespace FoodReport.DAL.Interfaces
{
    public interface IUserRepo : IRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}