using FoodReport.BLL.Interfaces.PasswordHashing;
using FoodReport.DAL.Data;
using FoodReport.DAL.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FoodReport.BLL.Services
{
    public class InitMongoDbService
    {
        private readonly MongoContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public InitMongoDbService(IOptions<Settings> options, IPasswordHasher passwordHasher)
        {
            _context = new MongoContext(options);
            _passwordHasher = passwordHasher;
        }

        private async void InsertRole(Role role, string field, string value)
        {
            var filterRole = Builders<Role>.Filter.Eq(field, value);

            var usr = await _context.Roles
                .Find(filterRole)
                .FirstOrDefaultAsync();
            if (usr == null) await _context.Roles.InsertOneAsync(role);
        }

        public async void Init()
        {
            var adminRole = new Role
            {
                Name = "Admin"
            };

            InsertRole(adminRole, "Name", adminRole.Name);

            var userRole = new Role
            {
                Name = "User"
            };
            InsertRole(userRole, "Name", userRole.Name);

            var odmen = new User
            {
                Email = "admin@ad",
                Password = _passwordHasher.HashPassword("admin"),
                Role = adminRole.Name
            };
            var filter = Builders<User>.Filter.Eq("Email", odmen.Email);

            var usr = await _context.Users
                .Find(filter)
                .FirstOrDefaultAsync();
            if (usr == null) await _context.Users.InsertOneAsync(odmen);
        }
    }
}