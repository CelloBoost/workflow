using Rgvc.Api.Rest.Health.Contracts.Responses;

namespace Rgvc.Api.Rest.Health.Mappers;

public static class HealthMapper
{
    public static HealthResponse ToResponse()
    {
        return new HealthResponse();
    }
}
