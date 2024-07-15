using Products.Domain.Entities;

namespace Products.Domain.Interfaces.Services;

public interface IProductService : IService<Product>
{
    Task<IEnumerable<Product>> GetByNameAsync(string name);
}
