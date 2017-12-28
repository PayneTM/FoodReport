using System.Threading.Tasks;
using FoodReport.Common.Interfaces;

namespace FoodReport.BLL.Interfaces.UserManager
{
    public interface ICustomUserManager : IUserManager<IUser>
    {
        Task ChangePassword(string id, string password);

        Task ChangeEmail(string id, string email);

        Task ChangeRole(string id, string role);
    }
}