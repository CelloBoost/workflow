using Microsoft.AspNetCore.Mvc;
using Workflow.Api.Rest.Health.Contracts.Responses;
using Workflow.Api.Rest.Health.Mappers;

namespace Workflow.Api.Rest.Health;

[ApiController]
[Route(HealthRoutes.Resource)]
[Tags("Health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(HealthResponse), StatusCodes.Status200OK)]
    public ActionResult<HealthResponse> Get()
    {
        return Ok(HealthMapper.ToResponse());
    }
}
