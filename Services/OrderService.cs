using KitchenApi.DTO.Order;
using KitchenApi.Interfaces;
using KitchenApi.Models;
using KitchenApi.Requests;

namespace KitchenApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<bool> AcceptOrder(bool accept, string orderId, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderAsync(orderId,cancellationToken);
            if (order is not null)
            {
                if (accept)
                {
                    order.Status = "accepted";
                }
                else
                {
                    order.Status = "rejected";
                }
                return await _orderRepository.UpdateOrderAsync(order, cancellationToken);
            }
            return false;
        }

        public async Task<string?> AddOrderAsync(OrderCreateRequest orderForCreate, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                OrderNumber = orderForCreate.OrderNumber,
                FinishDateTime = orderForCreate.FinishDateTime,
                Status = "pending",
                Products = orderForCreate.Products,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now
            };

            var orderId = await _orderRepository.AddOrderAsync(order, cancellationToken);
            return orderId;
        }

        public async Task<bool> DeleteOrderAsync(string id, CancellationToken cancellationToken)
        {
            return await _orderRepository.DeleteOrderAsync(id, cancellationToken);
        }

        public async Task<IReadOnlyCollection<OrderForReturn>> GerOrdersAsync(CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllOrdersAsync(cancellationToken);
            var ordersForReturn = new List<OrderForReturn>();
            foreach (var order in orders)
            {
                ordersForReturn.Add(new OrderForReturn
                {
                    Status = order.Status,
                    Products = order.Products,
                    OrderNumber = order.OrderNumber,
                    FinishDateTime = order.FinishDateTime
                });
            }
            return ordersForReturn;
        }

        public async Task<OrderForReturn?> GetOrderAsync(string id, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderAsync(id, cancellationToken);
            return order is not null
                ? new OrderForReturn
                {
                    OrderNumber = order.OrderNumber,
                    FinishDateTime = order.FinishDateTime,
                    Products = order.Products,
                    Status = order.Status
                }
                : null;
        }

        public async Task<bool> UpdateOrderAsync(string id, OrderUpdateRequest order, CancellationToken cancellationToken)
        {
            var orderForUpdate = await _orderRepository.GetOrderAsync(id, cancellationToken);

            return orderForUpdate is not null 
                ? await _orderRepository.UpdateOrderAsync(orderForUpdate, cancellationToken)
                : false;
        }

        public async Task<bool> UpdateOrderAsync(Order order, CancellationToken cancellationToken)
        {
            return await _orderRepository.UpdateOrderAsync(order, cancellationToken);
        }
    }
}
