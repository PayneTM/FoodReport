using FoodReport.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using FoodReport.DAL.Models;
using System.Threading.Tasks;
using FoodReport.DAL.Data;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FoodReport.DAL.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly MongoContext _context = null;

        public UserRepo(IOptions<Settings> settings)
        {
            _context = new MongoContext(settings);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await _context.Users
                        .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<User> Get(string id)
        {
            var filter = Builders<User>.Filter.Eq("Id", id);

            try
            {
                return await _context.Users
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task Add(User item)
        {
            try
            {
                item.Role = "User";
                await _context.Users.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> Remove(string id)
        {
            try
            {
                DeleteResult actionResult
                    = await _context.Users.DeleteOneAsync(
                        Builders<User>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> Update(string id, User item)
        {
            try
            {
                ReplaceOneResult actionResult
                    = await _context.Users
                                    .ReplaceOneAsync(n => n.Id.Equals(id)
                                            , item
                                            , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<User> Get(string email, string password)
        {
            var filter = Builders<User>.Filter.Eq("Email", email);

            try
            {
                var usr = await _context.Users
                                .Find(filter)
                                .FirstOrDefaultAsync();
                if (usr.Password == password) return usr;
                return null;
            }
            catch 
            {
                // log or manage the exception
                return null;
            }
        }
    }
}
