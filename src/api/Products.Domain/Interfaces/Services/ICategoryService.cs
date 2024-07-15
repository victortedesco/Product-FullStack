using Products.Domain.Entities;

namespace Products.Domain.Interfaces.Services;

public interface ICategoryService : IService<Category>
{
    Task<Category?> GetByNameAsync(string name);
}
