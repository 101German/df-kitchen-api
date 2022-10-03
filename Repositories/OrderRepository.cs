using KitchenApi.Context;
using KitchenApi.Interfaces;
using KitchenApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace KitchenApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IKitchenContext _context;
        private readonly ILogger<OrderRepository> _logger;
        public OrderRepository(IKitchenContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<string?> AddOrderAsync(Order order, CancellationToken cancellationToken)
        {
            await _context.Orders.InsertOneAsync(order, cancellationToken);
            _logger.LogInformation(order.Id is not null
                ? $"order with id {order.Id} was created"
                : $"failed to create order");
            return order.Id;
        }

        public async Task<bool> DeleteOrderAsync(string id, CancellationToken cancellationToken)
        {
            var result = await _context.Orders.DeleteOneAsync(o => o.Id == id, cancellationToken);
            var isDeletedSuccessfully = result.IsAcknowledged && result.DeletedCount > 0;
            _logger.LogInformation(isDeletedSuccessfully
                ? $"Order with id: {id} was successfuly deleted"
                : $"Failed to delete order with id:{id}");
            return isDeletedSuccessfully;
        }

        public async Task<IReadOnlyCollection<Order?>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Find(o => true).ToListAsync();
            _logger.LogInformation(orders.Any()
                ? $"all orders were retrieved"
                : $"orders are't exist");
            return orders;
        }

        public async Task<Order?> GetOrderAsync(string id, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.Find(o => o.Id == id).SingleOrDefaultAsync();
            _logger.LogInformation(order is not null
                ? $"order with id:{id} was retrieved"
                : $"order with id:{id} was't retrieved");
            return order;
        }

        public async Task<bool> UpdateOrderAsync(Order order, CancellationToken cancellationToken)
        {
            var result = await _context.Orders.ReplaceOneAsync(o => o.Id == order.Id, order);
            var isUpdatedSuccessfuly = result.IsAcknowledged && result.ModifiedCount > 0;

            _logger.LogInformation(isUpdatedSuccessfuly
                ? $"Order with id: {order.Id} was successfuly updated"
                : $"Failed to update order with id:{order.Id}");
            return isUpdatedSuccessfuly;
        }
    }
}
