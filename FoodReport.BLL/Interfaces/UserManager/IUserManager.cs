using System.Threading.Tasks;

namespace FoodReport.BLL.Interfaces.UserManager
{
    public interface IUserManager<TUser>
    {
        Task<TUser> Create(TUser user, string role);
        Task Edit(TUser user);
        Task Delete(string id);
        Task<TUser> GetById(string id);
        Task<TUser> GetByName(string name);
        Task<TUser> PasswordValidate(TUser user);
    }
}