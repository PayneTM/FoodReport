using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FoodReport.DAL.Abstractions
{
    public abstract class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}