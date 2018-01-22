using System;
using System.Linq;
using System.Threading.Tasks;
using FoodReport.BLL.Interfaces.PasswordHashing;
using FoodReport.BLL.Interfaces.UserManager;
using FoodReport.Common.Interfaces;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;

namespace FoodReport.BLL.Services
{
    public class UserManager : ICustomUserManager
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public UserManager(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<User> Create(User user, string role)
        {
            try
            {
                var result = await _unitOfWork.Users().GetByEmail(user.Email);
                if (result == null)
                {
                    var userRole = await _unitOfWork.Roles().FindRoleByName(role);
                    var usr = new User
                    {
                        Email = user.Email,
                        Password = _passwordHasher.HashPassword(user.Password),
                        Role = userRole.Name
                    };
                    await _unitOfWork.Users().Add(usr);
                    return usr;
                }

                throw new Exception();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task Edit(User user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(string id)
        {
            try
            {
                return await _unitOfWork.Users().Get(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<User> GetByName(string name)
        {
            try
            {
                var list = await _unitOfWork.Users().GetAll();
                return list.FirstOrDefault(x => x.Email == name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<User> PasswordValidate(User user)
        {
            try
            {
                var usr = await _unitOfWork.Users().GetByEmail(user.Email);
                var pswd = _passwordHasher.HashPassword(user.Password);
                return usr.Password == pswd ? usr : null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}