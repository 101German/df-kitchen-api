using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace KitchenApi.Models
{
    public class Status
    {
        public int Id { get; set; } 
        public string Title { get; set; }
    }
}
