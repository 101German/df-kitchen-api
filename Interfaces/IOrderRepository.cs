using KitchenApi.Models;

namespace KitchenApi.Interfaces
{
    public interface IOrderRepository
    {
        Task<IReadOnlyCollection<Order?>> GetAllOrdersAsync(CancellationToken cancellationToken);
        Task<Order?> GetOrderAsync(string id, CancellationToken cancellationToken);
        Task<string?> AddOrderAsync(Order order, CancellationToken cancellationToken);
        Task<bool> DeleteOrderAsync(string id, CancellationToken cancellationToken);
        Task<bool> UpdateOrderAsync(Order order, CancellationToken cancellationToken);
    }
}
