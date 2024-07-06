using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;
using Products.Domain.Interfaces;
using Products.Domain.Interfaces.Repository;
using Products.Infrastructure.Data;

namespace Products.Infrastructure.Repository;

public class ProductRepository : IProductRepository
{
    private readonly DbSet<Product> _products;
    private readonly IUnitOfWork _unitOfWork;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _products = dbContext.Products;
        _unitOfWork = new UnitOfWork(dbContext);
    }

    public IUnitOfWork UnitOfWork { get => _unitOfWork; }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _products.AsNoTracking().ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetByNameAsync(string name)
    {
        return await _products.AsNoTracking()
            .Where(p => p.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }

    public async Task<string> AddAsync(Product product)
    {
        if (!Product.IsValidName(product.Name))
            return "Invalid product name.";
        if (!Product.IsValidDescription(product.Description))
            return "Invalid product description.";
        if (!Product.IsValidPrice(product.Price))
            return "Invalid product price.";
        if (!Product.IsValidDiscount(product.Discount))
            return "Invalid product discount.";

        await _products.AddAsync(product);

        return "Ok";
    }

    public async Task<string> UpdateAsync(Guid id, Product productRequest)
    {
        var product = await _products.FindAsync(id);

        if (product is null)
            return "Not Found";

        product.UpdateImageUrl(productRequest.ImageUrl);
        if (!product.UpdateName(productRequest.Name))
            return "Invalid product name.";
        if (!product.UpdateDescription(productRequest.Description))
            return "Invalid product description.";
        if (!product.UpdatePrice(productRequest.Price))
            return "Invalid product price.";
        if (!product.UpdateDiscount(productRequest.Discount))
            return "Invalid product discount.";

        _products.Update(product);
        return "Ok";
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _products.FindAsync(id);

        if (product is null)
            return false;

        _products.Remove(product);
        return true;
    }
}

