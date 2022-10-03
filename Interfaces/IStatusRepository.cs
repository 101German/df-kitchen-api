using KitchenApi.Models;

namespace KitchenApi.Interfaces
{
    public interface IStatusRepository
    {
        Task<Status?> GetStatusAsync(string id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Status>> GetAllStatusesAsync(CancellationToken cancellationToken);
        Task<string?> AddStatusAsync(Status status,CancellationToken cancellationToken);
        Task<bool> IsStatusExistAsync(string title, CancellationToken cancellationToken);
    }
}
