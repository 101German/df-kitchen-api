using KitchenApi.Models;

namespace KitchenApi.Interfaces
{
    public interface IStatusService
    {
        Task<bool> AddStatusAsync(string statusTitle, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Status>> GetAllStatusesAsync(CancellationToken cancellationToken);
        Task<Status> GetStatusAsync(string id,CancellationToken cancellationToken);
        Task<bool> IsStatusExistAsync(string title, CancellationToken cancellationToken);
    }
}
