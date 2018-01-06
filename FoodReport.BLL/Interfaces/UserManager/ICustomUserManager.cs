using FoodReport.DAL.Models;
using System.Threading.Tasks;

namespace FoodReport.BLL.Interfaces.UserManager
{
    public interface ICustomUserManager : IUserManager<User>
    {
        Task ChangePassword(string id, string password);

        Task ChangeEmail(string id, string email);

        Task ChangeRole(string id, string role);
    }
}