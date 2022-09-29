using KitchenApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KitchenApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<IActionResult> Statuses(CancellationToken cancellationToken = default)
        {
            var statuses = await _statusService.GetAllStatusesAsync(cancellationToken);
            return statuses is not null ? Ok(statuses) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Status(string title, CancellationToken cancellationToken = default)
        {
            var result = await _statusService.AddStatusAsync(title, cancellationToken);

            return result ? Ok() : BadRequest();
        }
    }
}
