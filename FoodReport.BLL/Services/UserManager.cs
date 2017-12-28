using FoodReport.BLL.Interfaces.PasswordHashing;
using FoodReport.BLL.Interfaces.UserManager;
using FoodReport.DAL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using FoodReport.Common.Interfaces;
using FoodReport.DAL.Models;

namespace FoodReport.BLL.Services
{
    public class UserManager //: ICustomUserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        public UserManager(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
        }

        public async Task<IUser> Create(IUser user, string role)
        {
            try
            {
                var result = await _unitOfWork.Users().GetByEmail(user.Email);
                if (result != null) throw new Exception("Email already used");
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Task Edit(IUser user, string id)
        {
            //if(id == null || user == null) throw new Exception("Invalid parameters");
            //var usr = await GetById(id);
            //await _unitOfWork.Users().Update(id, usr);
            throw new NotImplementedException();

        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IUser> GetById(string id)
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

        public async Task<IUser> GetByEmail(string email)
        {
            #region [*Doubts*]
            //??????????????????????
            // get all --> database
            // list <-- database
            // selection (list) --> result
            //
            // ?????????????????????
            //
            // get by criteria --> database
            // list <-- database
            // list == result
            //??????????????????????
            #endregion
            try
            {
                var list = await _unitOfWork.Users().GetAll();
                var usr = list.FirstOrDefault(x => x.Email == email);
                if(usr == null) throw new Exception("User not found");
                return usr;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public async Task<IUser> PasswordValidate(IUser user)
        {
            try
            {
                var usr = await GetByEmail(user.Email);
                var pswd = _passwordHasher.HashPassword(user.Password);
                return usr.Password == pswd ? usr : throw new Exception("Email or password not match");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
