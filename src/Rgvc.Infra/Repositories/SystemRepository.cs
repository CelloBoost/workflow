using Microsoft.EntityFrameworkCore;
using Rgvc.Domain.Abstractions;
using Rgvc.Domain.Models;
using Rgvc.Infra.Data;

namespace Rgvc.Infra.Repositories;

public class SystemRepository : ISystemRepository
{
    private readonly RgvcDbContext _dbContext;

    public SystemRepository(RgvcDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<SystemData?> GetAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SystemData.AsNoTracking().SingleOrDefaultAsync(cancellationToken);
    }
}
