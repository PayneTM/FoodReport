using FoodReport.DAL.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FoodReport.DAL.Data
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
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

        public IMongoCollection<Role> Roles
        {
            get
            {
                try
                {
                    return _database.GetCollection<Role>("Roles");
                }
                catch
                {
                    _database.CreateCollection("Roles");
                    return _database.GetCollection<Role>("Roles");
                }
            }
        }
    }
}