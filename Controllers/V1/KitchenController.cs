using KitchenApi.Interfaces;
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
        private readonly IMongoCollection<Order> _orders;
        private readonly IMongoCollection<Status> _statuses;

        public KitchenController(IKitchenDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _orders = database.GetCollection<Order>(settings.OrdersCollectionName);
            _statuses = database.GetCollection<Status>(settings.StatusesCollectionName);
        }
        [HttpGet]
        public async Task<IActionResult> Order()
        {
            var orders = await _orders.FindAsync(o => true);

            return Ok(await orders.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Order(OrderCreateRequest orderForCreate)
        {
            var status = await _statuses.FindAsync(s => s.Id == 1).Result.FirstOrDefaultAsync();

            var order = new Order
            {
                OrderNumber = orderForCreate.OrderNumber,
                FinishDateTime = orderForCreate.FinishDateTime,
                Status = status.Title,
                Products = orderForCreate.Products
            };
            await _orders.InsertOneAsync(order);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> AcceptOrder(bool accept, string id)
        {
            var order = await _orders.FindAsync(o => o.Id == id).Result.FirstOrDefaultAsync();
            if (accept)
            {
                var acceptStatus = await _statuses.FindAsync(s => s.Id == 2).Result.FirstOrDefaultAsync();
                var acceptedOrder = new Order
                {
                    OrderNumber = order.OrderNumber,
                    FinishDateTime = order.FinishDateTime,
                    Status = acceptStatus.Title,
                    Products = order.Products
                };
                await _orders.InsertOneAsync(acceptedOrder);
            }
            else
            {
                var rejectStatus = await _statuses.FindAsync(s => s.Id == 4).Result.FirstOrDefaultAsync();
                var rejectedOrder = new Order
                {
                    OrderNumber = order.OrderNumber,
                    FinishDateTime = order.FinishDateTime,
                    Status = rejectStatus.Title,
                    Products = order.Products
                };
                await _orders.InsertOneAsync(rejectedOrder);
            }
            return Ok();
        }
     }
}
