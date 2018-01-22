using System.Threading.Tasks;
using FoodReport.Common.Interfaces;

namespace FoodReport.BLL.Interfaces.UserManager
{
    public interface IUserManager<TUser> where TUser : IUser
    {
        Task<TUser> Create(TUser user, string role);
        Task Edit(TUser user);
        Task Delete(string id);
        Task<TUser> GetById(string id);
        Task<TUser> GetByName(string name);
        Task<TUser> PasswordValidate(TUser user);
    }
}