using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using KitchenApi.Requests;

namespace KitchenApi.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public int OrderNumber { get; set; }
        public DateTime FinishDateTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
