using Rgvc.Application.Abstractions;
using Rgvc.Domain.Abstractions;

namespace Rgvc.Application.Services;

public class SystemService : ISystemService
{
    private readonly ISystemRepository _systemRepository;

    public SystemService(ISystemRepository systemRepository)
    {
        _systemRepository = systemRepository;
    }

    public async Task<string> GetVersionAsync(CancellationToken cancellationToken = default)
    {
        var systemData = await _systemRepository.GetAsync(cancellationToken);
        if (systemData is null)
        {
            throw new InvalidOperationException("System data was not initialized.");
        }

        return systemData.Version;
    }
}
