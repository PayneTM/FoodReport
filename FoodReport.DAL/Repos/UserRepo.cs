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

        public async Task<IEnumerable<User>> GetAll(string id = null)
        {
            try
            {
                return await _context.Users
                        .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
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
                throw ex;
            }
        }

        public async Task Add(User item)
        {
            var filter = Builders<User>.Filter.Eq("Email", item.Email);

            try
            {
                 var usr = await _context.Users
                    .Find(filter)
                    .FirstOrDefaultAsync();
                if (usr == null)
                {
                    await _context.Users.InsertOneAsync(item);
                }
            }
            catch (Exception ex)
            {
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
                throw ex;
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq("Email", email);

            try
            {
                var usr = await _context.Users
                                .Find(filter)
                                .FirstOrDefaultAsync();
                //if (usr.Password == password) return usr;
                return usr;
            }
            catch 
            {
                return null;
            }

        }
    }
}
