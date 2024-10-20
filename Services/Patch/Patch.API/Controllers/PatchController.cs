using EventBus.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Patch.Business.IntegrationEvents.Events;

namespace Patch.API.Controllers
{
    // NOTTODO not the best but looks good
    [Route("/")]
    [ApiController]
    public class PatchController(IIntegrationEventService patchIntegrationEventService) : ControllerBase
    {
        [HttpGet]
        [Route("ping")]
        public IActionResult Ping()
        {
            patchIntegrationEventService.Publish(new PingCalledIntegrationEvent());

            return Ok(DateTime.Now);
        }
    }
}
