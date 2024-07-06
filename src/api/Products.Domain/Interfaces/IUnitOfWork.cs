namespace Products.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task SaveAsync(CancellationToken cancellationToken = default);
}
