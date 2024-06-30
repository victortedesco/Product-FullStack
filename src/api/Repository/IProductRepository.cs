using ProductAPI.Models;

namespace ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetAll();
        Task<ProductModel> GetById(Guid id);
        Task<IEnumerable<ProductModel>> GetByName(string name);

        Task<bool> Add(ProductModel model);
        Task<bool> Update(Guid id, ProductModel model);
        Task<bool> Delete(Guid id);
    }
}
