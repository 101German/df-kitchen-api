namespace KitchenApi.Interfaces
{
    public interface IKitchenDatabaseSettings
    {
        string OrdersCollectionName { get; set; }
        string StatusesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
