using KitchenApi.Context;
using KitchenApi.Interfaces;
using KitchenApi.Models;
using MongoDB.Driver;

namespace KitchenApi.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly IKitchenContext _context;
        private readonly ILogger<StatusRepository> _logger;
        public StatusRepository(IKitchenContext context, ILogger<StatusRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<string?> AddStatusAsync(Status status, CancellationToken cancellationToken)
        {
            await _context.Statuses.InsertOneAsync(status, cancellationToken);

            _logger.LogInformation(status.Id is not null 
                ? $"status '{status.Title}' was created." 
                : $"status '{status.Title}' was not created.");
            return status.Id;
        }

        public async Task<IReadOnlyCollection<Status>> GetAllStatusesAsync(CancellationToken cancellationToken)
        {
            var statuses = await _context.Statuses.Find(s => true).ToListAsync(cancellationToken);
            _logger.LogInformation(statuses.Any()
               ? $"all statuses were retrieved"
               : $"all statuses were not retrieved");
            return statuses;
        }

        public async Task<Status?> GetStatusAsync(string id, CancellationToken cancellationToken)
        {
            var status = await _context.Statuses.Find(s => s.Id.Equals(id)).SingleOrDefaultAsync(cancellationToken);
            _logger.LogInformation(status is not null
               ? $"status with {id} was retrieved"
               : $"status with {id} not retrieved");
            return status;
        }

        public async Task<bool> IsStatusExistAsync(string title, CancellationToken cancellationToken)
        {
            var isStatusExist = await _context.Statuses.Find(s => s.Title == title).AnyAsync(cancellationToken);
            _logger.LogInformation(isStatusExist 
                ? $"status with title '{title}' is exist" 
                : $"status with title '{title}' is not exist");
            return isStatusExist;
        }
    }
}
