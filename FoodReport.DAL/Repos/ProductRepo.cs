using FoodReport.DAL.Data;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodReport.DAL.Repos
{
    public class ProductRepo : IProductRepository
    {
        private readonly MongoContext _context = null;

        public ProductRepo(IOptions<Settings> settings)
        {
            _context = new MongoContext(settings);
        }

        public async Task<IEnumerable<Product>> GetAll(string id = null)
        {
            try
            {
                return await _context.Products
                        .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Product> Get(string id)
        {
            var filter = Builders<Product>.Filter.Eq("Id", id);

            try
            {
                return await _context.Products
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task Add(Product item)
        {
            try
            {
                if(! await IsExisting("Name",item.Name))
               await _context.Products.InsertOneAsync(item);
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
                    = await _context.Products.DeleteOneAsync(
                        Builders<Product>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> Update(string id, Product item)
        {
            try
            {
                if(! await IsExisting("Name", item.Name))
                {

                ReplaceOneResult actionResult
                    = await _context.Products
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
                // log or manage the exception
                throw ex;
            }
        }

        private async Task<bool> IsExisting(string param, string search)
        {
            var filter = Builders<Product>.Filter.Eq(param, search);

            try
            {
                var item = await _context.Products
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
    }
}