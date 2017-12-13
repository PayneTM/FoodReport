using FoodReport.DAL.Data;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodReport.DAL.Repos
{
    public class RoleRepo : IRoleRepo
    {
        private readonly MongoContext _context = null;

        public RoleRepo(IOptions<Settings> settings)
        {
            _context = new MongoContext(settings);
        }

        public async Task<IEnumerable<Role>> GetAll(string id = null)
        {
            try
            {
                return await _context.Roles
                        .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Role> Get(string id)
        {
            var filter = Builders<Role>.Filter.Eq("Id", id);

            try
            {
                return await _context.Roles
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Add(Role item)
        {
            try
            {
                await _context.Roles.InsertOneAsync(item);
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
                    = await _context.Roles.DeleteOneAsync(
                        Builders<Role>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(string id, Role item)
        {
            try
            {
                if (!await IsExisting("Name", item.Name))
                {

                    ReplaceOneResult actionResult
                        = await _context.Roles
                                        .ReplaceOneAsync(n => n.Id.Equals(id)
                                                , item
                                                , new UpdateOptions { IsUpsert = true });
                    return actionResult.IsAcknowledged
                        && actionResult.ModifiedCount > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<bool> IsExisting(string param, string search)
        {
            var filter = Builders<Role>.Filter.Eq(param, search);

            try
            {
                var item = await _context.Roles
                                .Find(filter)
                                .FirstOrDefaultAsync();
                if (item != null) return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Role> FindRoleByName(string name)
        {
            var filter = Builders<Role>.Filter.Eq("Name", name);

            try
            {
                return await _context.Roles
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
