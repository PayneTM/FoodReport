using FoodReport.DAL.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FoodReport.DAL.Data
{
    public class InitMongoDb
    {
        private MongoContext _context;
        public InitMongoDb(IOptions<Settings> options)
        {
            _context = new MongoContext(options);
        }
        public async void Init()
        {
            var odmen = new User
            {
                Email = "admin@ad",
                Password = "admin",
                Role  = "Admin"
            };
            var filter = Builders<User>.Filter.Eq("Email", odmen.Email);

               var usr =  await _context.Users
                                .Find(filter)
                                .FirstOrDefaultAsync();
            if (usr == null)
            {
                await _context.Users.InsertOneAsync(odmen);
            }
        }
    }
}
