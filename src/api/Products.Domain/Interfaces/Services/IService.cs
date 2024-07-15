using FluentResults;

namespace Products.Domain.Interfaces.Services;

public interface IService<T> where T : class, IEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetByCreatedAtAsync(DateOnly date);
    Task<IEnumerable<T>> GetByUpdatedAtAsync(DateOnly date);

    Task<Result> AddAsync(T entity);
    Task<Result> UpdateAsync(Guid id, T entity);
    Task<Result> DeleteAsync(Guid id);
}
