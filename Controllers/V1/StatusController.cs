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
    public class StatusController : ControllerBase
    {
        private readonly IMongoCollection<Status> _statuses;
        public StatusController(IKitchenDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _statuses = database.GetCollection<Status>(settings.StatusesCollectionName);
        }

        [HttpGet]
        public async Task<IActionResult> Statuses()
        {
            var statuses = await _statuses.FindAsync(s => true);
            return Ok(await statuses.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Status(StatusCreateRequest status)
        {
            var IsStatusExist = await IsStatusExistWithSameName(status.Title);
            if (!IsStatusExist)
            {
                var statusForCreate = new Status
                {
                    Title = status.Title
                };
                await _statuses.InsertOneAsync(statusForCreate);
                return Ok();
            }
            return BadRequest("Status is already exist with this name.");
        }

        private async Task<bool> IsStatusExistWithSameName(string title)
        {
            var status = await _statuses.FindAsync(s => s.Title.ToLower() == title.ToLower()).Result.FirstOrDefaultAsync();
            return status != null;
        }
    }
}
