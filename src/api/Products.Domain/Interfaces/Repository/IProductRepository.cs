using Products.Domain.Entities;

namespace Products.Domain.Interfaces.Repository;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByNameAsync(string name);
}
