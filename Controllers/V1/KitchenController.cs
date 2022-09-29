using KitchenApi.Context;
using KitchenApi.Models;
using KitchenApi.Requests;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace KitchenApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class KitchenController : ControllerBase 
    {
        private readonly IKitchenContext _context;

        public KitchenController(IKitchenContext context)
        {
           _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Order()
        {
            var orders = await _context.Orders.FindAsync(o => true);
            return Ok(await orders.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Order(OrderCreateRequest orderForCreate)
        {
            var order = new Order
            {
                OrderNumber = orderForCreate.OrderNumber,
                FinishDateTime = orderForCreate.FinishDateTime,
                Status = "pending",
                Products = orderForCreate.Products
            };
            await _context.Orders.InsertOneAsync(order);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> AcceptOrder(bool accept, string id)
        {
            var order = await _context.Orders.FindAsync(o => o.Id == id).Result.FirstOrDefaultAsync();
            if (accept)
            {
                var acceptedOrder = new Order
                {
                    OrderNumber = order.OrderNumber,
                    FinishDateTime = order.FinishDateTime,
                    Status = "accepted",
                    Products = order.Products
                };
                await _context.Orders.InsertOneAsync(acceptedOrder);
            }
            else
            {
                var rejectedOrder = new Order
                {
                    OrderNumber = order.OrderNumber,
                    FinishDateTime = order.FinishDateTime,
                    Status = "rejected",
                    Products = order.Products
                };
                await _context.Orders.InsertOneAsync(rejectedOrder);
            }
            return Ok();
        }
     }
}
