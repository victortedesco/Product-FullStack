using Products.Domain.Entities;

namespace Products.Domain.Interfaces.Repository;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByNameAsync(string name);
}
