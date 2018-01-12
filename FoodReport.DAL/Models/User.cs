﻿using System.ComponentModel.DataAnnotations;
using FoodReport.Common.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FoodReport.DAL.Models
{
    public class User : IUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }

        public string Role { get; set; }
    }
}