using Workflow.Domain.Models;

namespace Workflow.Domain.Abstractions;

public interface ISystemRepository
{
    Task<SystemData?> GetAsync(CancellationToken cancellationToken = default);
}
