using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodReport.DAL.Data;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FoodReport.DAL.Repos
{
    public class ReportRepo : IReportRepository
    {
        private readonly MongoContext _context;

        public ReportRepo(IOptions<Settings> settings)
        {
            _context = new MongoContext(settings);
        }

        public async Task<IEnumerable<Report>> GetAll(string id = null)
        {
            try
            {
                return await _context.Reports
                    .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Report> Get(string id)
        {
            var filter = Builders<Report>.Filter.Eq("Id", id);

            try
            {
                return await _context.Reports
                    .Find(filter)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Add(Report item)
        {
            try
            {
                await _context.Reports.InsertOneAsync(item);
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
                var actionResult
                    = await _context.Reports.DeleteOneAsync(
                        Builders<Report>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                       && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(string id, Report item)
        {
            try
            {
                var actionResult
                    = await _context.Reports
                        .ReplaceOneAsync(n => n.Id.Equals(id)
                            , item
                            , new UpdateOptions {IsUpsert = true});
                return actionResult.IsAcknowledged
                       && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}