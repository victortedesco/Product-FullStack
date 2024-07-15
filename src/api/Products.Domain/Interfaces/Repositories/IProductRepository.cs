using Products.Domain.Entities;

namespace Products.Domain.Interfaces.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByNameAsync(string name);
}
