using System.Collections.Generic;
using System.Threading.Tasks;
using FoodReport.Common.Interfaces;

namespace FoodReport.BLL.Interfaces.UserManager
{
    public interface IUserManager<TUser>
    {
        Task<TUser> Create(TUser user, string role);
        Task Edit(TUser user, string id);
        Task Delete(string id);
        Task<TUser> GetById(string id);
        Task<TUser> GetByName(string name);
        Task<TUser> GetByEmail(string email);
        Task<TUser> PasswordValidate(TUser user);
        Task<IEnumerable<IUser>> GetAllUsers();
    }
}