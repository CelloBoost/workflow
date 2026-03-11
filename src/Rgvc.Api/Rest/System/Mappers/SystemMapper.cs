using Rgvc.Api.Rest.System.Contracts.Responses;

namespace Rgvc.Api.Rest.System.Mappers;

public static class SystemMapper
{
    public static SystemResponse ToResponse(string version)
    {
        return new SystemResponse { Version = version };
    }
}
