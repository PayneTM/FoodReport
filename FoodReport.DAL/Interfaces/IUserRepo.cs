using FoodReport.DAL.Models;
using System.Threading.Tasks;
using FoodReport.Common.Interfaces;

namespace FoodReport.DAL.Interfaces
{
    public interface IUserRepo : IRepository<IUser>
    {
        Task<IUser> GetByEmail(string email);
    }
}
