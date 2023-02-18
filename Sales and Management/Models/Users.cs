using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sales_and_Management.Models
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement]
        public string name { get; set; }

        [BsonElement]
        public string userName { get; set; }

        [BsonElement]
        public string email { get; set; }
    }
}
