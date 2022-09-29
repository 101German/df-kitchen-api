using KitchenApi.Models;
using MongoDB.Driver;

namespace KitchenApi.Context
{
    public interface IKitchenContext
    {
        public IMongoCollection<Order> Orders { get; set; }
        public IMongoCollection<Status> Statuses { get; set; }
    }
}
