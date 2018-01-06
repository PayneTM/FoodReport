using FoodReport.BLL.Interfaces.PasswordHashing;
using FoodReport.BLL.Interfaces.UserManager;
using FoodReport.Common.Interfaces;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodReport.BLL.Services
{
    public class UserManager : ICustomUserManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
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
                if (result != null) throw new Exception("Email already used");
                user.Password = _passwordHasher.HashPassword(user.Password);
                user.Role = role;
                //var usr = new User
                //{
                //    Email = user.Email,
                //    Password = _passwordHasher.HashPassword(user.Password),
                //    Role = role
                //};
                await _unitOfWork.Users().Add(user);
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        public async Task Edit(User user, string id)
        {
            try
            {
                if (id == null || user == null || user.Id != id) throw new Exception("Invalid parameters");
                await _unitOfWork.Users().Update(id, user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                if (id == null) throw new ArgumentNullException();
                var result = await _unitOfWork.Users().Remove(id);
                if (result == false) throw new Exception("User not found");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
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
                return await GetByEmail(name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<User> GetByEmail(string email)
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
                if (usr == null) throw new Exception("User not found");
                return usr;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<User> PasswordValidate(User user)
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

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return await _unitOfWork.Users().GetAll();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task ChangePassword(string id, string password)
        {
            try
            {
            //todo: rename variables usr and user
                if (id == null || password == null) throw new ArgumentNullException();
                var usr = await GetById(id);
                var user = new User
                {
                    Email = usr.Email,
                    Role = usr.Role,
                    Id = usr.Id,
                    Password = _passwordHasher.HashPassword(password)
                };
                await Edit(user, id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task ChangeEmail(string id, string email)
        {
            try
            {
                //todo: rename variables usr and user
                if (id == null || email == null) throw new ArgumentNullException();
                var usr = await GetById(id);
                var user = new User
                {
                    Email = email,
                    Role = usr.Role,
                    Id = usr.Id,
                    Password = usr.Password
                };
                await Edit(user, id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async Task ChangeRole(string id, string role)
        {
            try
            {
                if (id == null || role == null) throw new ArgumentNullException();
                //var userRole = await _unitOfWork.Roles().FindRoleByName(role);
                //if (userRole == null) throw new Exception("There is no this role");
                //todo: rename variables usr and user

                var usr = await GetById(id);
                var user = new User
                {
                    Email = usr.Email,
                    Role = role,
                    Id = usr.Id,
                    Password = usr.Password
                };
                await Edit(user, id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
