using Microsoft.EntityFrameworkCore;
using Workflow.Domain.Abstractions;
using Workflow.Domain.Models;
using Workflow.Infra.Data;

namespace Workflow.Infra.Repositories;

public class SystemRepository : ISystemRepository
{
    private readonly WorkflowDbContext _dbContext;

    public SystemRepository(WorkflowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<SystemData?> GetAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SystemData.AsNoTracking().SingleOrDefaultAsync(cancellationToken);
    }
}
