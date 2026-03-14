using Workflow.Api.Rest.System.Contracts.Responses;

namespace Workflow.Api.Rest.System.Mappers;

public static class SystemMapper
{
    public static SystemResponse ToResponse(string version)
    {
        return new SystemResponse { Version = version };
    }
}
