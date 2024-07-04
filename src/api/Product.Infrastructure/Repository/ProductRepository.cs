using Microsoft.EntityFrameworkCore;
using Product.Domain.Interfaces.Repository;
using Product.Domain.Models;
using Product.Infrastructure.Data;

namespace Product.Infrastructure.Repository
{
    public class ProductRepository(ApplicationDataContext applicationDataContext) : IProductRepository
    {
        private readonly DbSet<ProductModel> _products = applicationDataContext.Products;

        public async Task<IEnumerable<ProductModel>> GetAll()
        {
            return await _products.AsNoTracking().ToListAsync();
        }

        public async Task<ProductModel?> GetById(Guid id)
        {
            return await _products.FindAsync(id);
        }

        public async Task<IEnumerable<ProductModel>> GetByName(string name)
        {
            return await _products
                .AsNoTracking()
                .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task<bool> Add(ProductModel product)
        {
            if (!ProductModel.IsValidProduct(product)) return false;

            await _products.AddAsync(product);
            return await applicationDataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(Guid id, ProductModel productRequest)
        {
            if (!ProductModel.IsValidProduct(productRequest)) return false;

            var product = await _products.FindAsync(id);

            if (product == null) return false;

            product.ImageUrl = productRequest.ImageUrl;
            product.Name = productRequest.Name;
            product.Description = productRequest.Description;
            product.Price = productRequest.Price;
            product.Discount = productRequest.Discount;

            _products.Update(product);
            return await applicationDataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var product = await _products.FindAsync(id);

            if (product == null) return false;

            _products.Remove(product);
            return await applicationDataContext.SaveChangesAsync() > 0;
        }
    }
}
