namespace Products.Domain.Interfaces.Repositories;

public interface IRepository<T> where T : class, IEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetByCreatedAtAsync(DateOnly date);
    Task<IEnumerable<T>> GetByUpdatedAtAsync(DateOnly date);

    void Add(T entity);
    void Update(Guid id, T entity);
    void Delete(T entity);
}
