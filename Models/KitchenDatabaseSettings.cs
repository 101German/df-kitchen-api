using KitchenApi.Interfaces;

namespace KitchenApi.Models
{
    public class KitchenDatabaseSettings
    {
        public string OrdersCollectionName { get; set; }
        public string StatusesCollectionName { get; set; }

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
