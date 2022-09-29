using KitchenApi.Interfaces;
using KitchenApi.Models;

namespace KitchenApi.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<bool> AddStatusAsync(string statusTitle, CancellationToken cancellationToken)
        {
            if (!await IsStatusExistAsync(statusTitle,cancellationToken))
            {
                var status = new Status
                {
                    Title = statusTitle,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                };
                return true;
            }
            return false;
        }

        public async Task<IReadOnlyCollection<Status>> GetAllStatusesAsync(CancellationToken cancellationToken)
        {
           return await _statusRepository.GetAllStatusesAsync(cancellationToken);
        }

        public async Task<Status> GetStatusAsync(string id, CancellationToken cancellationToken)
        {
            return await _statusRepository.GetStatusAsync(id, cancellationToken);
        }

        public async Task<bool> IsStatusExistAsync(string title, CancellationToken cancellationToken)
        {
            return await _statusRepository.IsStatusExistAsync(title, cancellationToken);
        }
    }
}
