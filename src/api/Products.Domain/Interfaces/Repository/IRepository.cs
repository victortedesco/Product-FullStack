namespace Products.Domain.Interfaces.Repository;

public interface IRepository<T> where T : class, IEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);

    Task<string> AddAsync(T model);
    Task<string> UpdateAsync(Guid id, T model);
    Task<bool> DeleteAsync(Guid id);

    IUnitOfWork UnitOfWork { get; }
}
