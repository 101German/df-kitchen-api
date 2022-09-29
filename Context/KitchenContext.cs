using KitchenApi.Models;
using MongoDB.Driver;

namespace KitchenApi.Context
{
    public class KitchenContext : IKitchenContext
    {
        public KitchenContext(KitchenDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            Orders = database.GetCollection<Order>(settings.OrdersCollectionName);
            Statuses = database.GetCollection<Status>(settings.StatusesCollectionName);
        }
        public IMongoCollection<Order> Orders { get; set; }
        public IMongoCollection<Status> Statuses { get; set; }
    }
}
