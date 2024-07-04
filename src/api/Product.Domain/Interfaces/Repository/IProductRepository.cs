using Product.Domain.Models;

namespace Product.Domain.Interfaces.Repository
{
    public interface IProductRepository : IRepository<ProductModel>
    {
        Task<IEnumerable<ProductModel>> GetByName(string name);
    }
}
