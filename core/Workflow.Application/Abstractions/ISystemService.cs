namespace Workflow.Application.Abstractions;

public interface ISystemService
{
    Task<string> GetVersionAsync(CancellationToken cancellationToken = default);
}
