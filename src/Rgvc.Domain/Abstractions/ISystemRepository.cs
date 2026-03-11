using Rgvc.Domain.Models;

namespace Rgvc.Domain.Abstractions;

public interface ISystemRepository
{
    Task<SystemData?> GetAsync(CancellationToken cancellationToken = default);
}
