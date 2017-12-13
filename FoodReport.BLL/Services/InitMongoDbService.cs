using FoodReport.BLL.Interfaces;
using FoodReport.DAL.Data;
using FoodReport.DAL.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodReport.BLL.Services
{
    public class InitMongoDbService
    {
        private MongoContext _context;
        private readonly IPasswordHasher _passwordHasher;
        public InitMongoDbService(IOptions<Settings> options, IPasswordHasher passwordHasher)
        {
            _context = new MongoContext(options);
            _passwordHasher = passwordHasher;
        }
        public async void Init()
        {
            var odmen = new User
            {
                Email = "admin@ad",
                Password = _passwordHasher.HashPassword("admin"),
                Role = "Admin"
            };
            var filter = Builders<User>.Filter.Eq("Email", odmen.Email);

            var usr = await _context.Users
                             .Find(filter)
                             .FirstOrDefaultAsync();
            if (usr == null)
            {
                await _context.Users.InsertOneAsync(odmen);
            }
        }
    }
}
