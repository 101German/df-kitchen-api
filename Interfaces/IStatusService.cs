using KitchenApi.DTO.Status;

namespace KitchenApi.Interfaces
{
    public interface IStatusService
    {
        Task<bool> AddStatusAsync(string statusTitle, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<StatusForReturn>> GetAllStatusesAsync(CancellationToken cancellationToken);
        Task<StatusForReturn?> GetStatusAsync(string id,CancellationToken cancellationToken);
        Task<bool> IsStatusExistAsync(string title, CancellationToken cancellationToken);
    }
}
