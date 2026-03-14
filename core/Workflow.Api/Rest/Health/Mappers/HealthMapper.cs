using Workflow.Api.Rest.Health.Contracts.Responses;

namespace Workflow.Api.Rest.Health.Mappers;

public static class HealthMapper
{
    public static HealthResponse ToResponse()
    {
        return new HealthResponse();
    }
}
