using KitchenApi.DTO.Order;
using KitchenApi.Models;
using KitchenApi.Requests;

namespace KitchenApi.Interfaces
{
    public interface IOrderService
    {
        Task<string?> AddOrderAsync(OrderCreateRequest order, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<OrderForReturn>> GerOrdersAsync(CancellationToken cancellationToken);
        Task<OrderForReturn?> GetOrderAsync(string id, CancellationToken cancellationToken);
        Task<bool> DeleteOrderAsync(string id, CancellationToken cancellationToken);
        Task<bool> UpdateOrderAsync(string id, OrderUpdateRequest order, CancellationToken cancellationToken);
        Task<bool> UpdateOrderAsync(Order order, CancellationToken cancellationToken);
        Task<bool> AcceptOrder(bool accept, string orderId, CancellationToken cancellationToken);
    }
}
