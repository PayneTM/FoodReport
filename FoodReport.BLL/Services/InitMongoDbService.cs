﻿using System;
using FoodReport.BLL.Interfaces.UserManager;
using FoodReport.DAL.Data;
using FoodReport.DAL.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace FoodReport.BLL.Services
{
    public class InitMongoDbService
    {
        private readonly MongoContext _context;
        private readonly ICustomUserManager _userManager;
        public InitMongoDbService(IOptions<Settings> options, ICustomUserManager userManager)
        {
            _context = new MongoContext(options);
            _userManager = userManager;
        }
        private async void InsertRole(Role role,string field, string value)
        {
            var filterRole = Builders<Role>.Filter.Eq(field, value);

            var usr = await _context.Roles
                             .Find(filterRole)
                             .FirstOrDefaultAsync();
            if (usr == null)
            {
                await _context.Roles.InsertOneAsync(role);
            }
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

            await _userManager.Create(new User
            {
                Email = "admin@ad",
                Password = "admin",
            }, 
            role: adminRole.Name);
        }
    }
}
