using Microsoft.AspNetCore.Mvc;
using Workflow.Api.Rest.Shared.Contracts.Responses;
using Workflow.Api.Rest.System.Contracts.Responses;
using Workflow.Api.Rest.System.Mappers;
using Workflow.Application.Abstractions;

namespace Workflow.Api.Rest.System;

[ApiController]
[Route(SystemRoutes.Resource)]
[Tags("System")]
public class SystemController : ControllerBase
{
    private readonly ISystemService _systemService;

    public SystemController(ISystemService systemService)
    {
        _systemService = systemService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(SystemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SystemResponse>> Get(CancellationToken cancellationToken)
    {
        try
        {
            var version = await _systemService.GetVersionAsync(cancellationToken);
            return Ok(SystemMapper.ToResponse(version));
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ErrorResponse.From(ex.Message));
        }
    }
}
