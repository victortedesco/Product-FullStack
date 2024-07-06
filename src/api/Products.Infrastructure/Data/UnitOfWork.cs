using Products.Domain.Interfaces;

namespace Products.Infrastructure.Data;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

    public Task SaveAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
