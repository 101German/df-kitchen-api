using KitchenApi.Context;
using KitchenApi.Interfaces;
using KitchenApi.Requests;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Net;

namespace KitchenApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class KitchenController : ControllerBase 
    {
        private readonly IOrderService _orderService;

        public KitchenController(IKitchenContext context, IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
        {
            var orders = await _orderService.GerOrdersAsync(cancellationToken);
            return orders.Any() ? Ok() : BadRequest();
        }
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrder(string id, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetOrderAsync(id, cancellationToken);
            return order is not null ? Ok() : BadRequest();
        }
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddOrder(OrderCreateRequest orderForCreate, CancellationToken cancellationToken)
        {
            var orderId = await _orderService.AddOrderAsync(orderForCreate, cancellationToken);
            return orderId is not null ? Ok(orderId) : BadRequest();
        }
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AcceptOrder(string id,bool accept, CancellationToken cancellationToken)
        {
            var res = await _orderService.AcceptOrder(accept, id, cancellationToken);
            return res ? Ok() : BadRequest();
        }
        [HttpPut("UpdateOrder/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateOrder(string id, OrderUpdateRequest order,CancellationToken cancellationToken)
        {
            var res = await _orderService.UpdateOrderAsync(id, order, cancellationToken);
            return res ? Ok() : BadRequest();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteOrder(string id, CancellationToken cancellationToken)
        {
            var res = await _orderService.DeleteOrderAsync(id, cancellationToken);
            return res ? Ok() : BadRequest();
        }
     }
}
