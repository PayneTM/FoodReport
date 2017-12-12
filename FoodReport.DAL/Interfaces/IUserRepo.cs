using FoodReport.DAL.Models;
using System.Threading.Tasks;

namespace FoodReport.DAL.Interfaces
{
    public interface IUserRepo : IRepository<User>
    {
        Task<User> Get(string email, string password);
    }
}
