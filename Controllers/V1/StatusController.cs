using KitchenApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

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
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Status(CancellationToken cancellationToken = default)
        {
            var statuses = await _statusService.GetAllStatusesAsync(cancellationToken);
            return statuses.Count > 0 ? Ok(statuses) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Status([MaxLength(25)]string title, CancellationToken cancellationToken = default)
        {
            var result = await _statusService.AddStatusAsync(title, cancellationToken);

            return result ? Ok() : BadRequest();
        }
    }
}
