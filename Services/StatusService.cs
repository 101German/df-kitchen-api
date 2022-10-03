using KitchenApi.DTO.Status;
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
                await _statusRepository.AddStatusAsync(status, cancellationToken);
                return true;
            }
            return false;
        }

        public async Task<IReadOnlyCollection<StatusForReturn>> GetAllStatusesAsync(CancellationToken cancellationToken)
        {
            var statuses = await _statusRepository.GetAllStatusesAsync(cancellationToken);
            var statusesForReturn = new List<StatusForReturn>();
            foreach(var status in statuses)
            {
                statusesForReturn.Add(new StatusForReturn
                {
                    Title = status.Title
                });
            }
           return statusesForReturn;
        }

        public async Task<StatusForReturn?> GetStatusAsync(string id, CancellationToken cancellationToken)
        {
            var status = await _statusRepository.GetStatusAsync(id, cancellationToken);
            return status != null 
                ? new StatusForReturn { Title = status.Title} 
                : null;
        }

        public async Task<bool> IsStatusExistAsync(string title, CancellationToken cancellationToken)
        {
            return await _statusRepository.IsStatusExistAsync(title, cancellationToken);
        }
    }
}
