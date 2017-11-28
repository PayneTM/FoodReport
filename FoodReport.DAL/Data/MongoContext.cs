using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FoodReport.DAL.Data
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
            try
            {
                _database.GetCollection<User>("Users");
            }
            catch
            {
                _database.CreateCollection("Users");
                var usrCollection = _database.GetCollection<User>("Users");
                var odmen = new User
                {
                    Email = "admin@admin",
                    Password = "admin"
                };
                usrCollection.InsertOne(odmen);
            }
        }

        public IMongoCollection<Product> Products
        {
            get
            {
                try
                {
                    return _database.GetCollection<Product>("Products");
                }
                catch
                {
                    _database.CreateCollection("Products");
                    return _database.GetCollection<Product>("Products");
                }
            }
        }
        public IMongoCollection<Report> Reports
        {
            get
            {
                try
                {
                    return _database.GetCollection<Report>("Reports");
                }
                catch
                {
                    _database.CreateCollection("Reports");
                    return _database.GetCollection<Report>("Reports");
                }
            }
        }

        public IMongoCollection<User> Users
        {
            get
            {
                try
                {
                    return _database.GetCollection<User>("Users");
                }
                catch
                {
                    _database.CreateCollection("Users");
                    return _database.GetCollection<User>("Users");
                }
            }
        }
    }
}
