using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace KitchenApi.Models
{
    public class BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
