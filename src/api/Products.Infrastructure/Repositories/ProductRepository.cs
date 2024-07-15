using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;
using Products.Infrastructure.Context;

namespace Products.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly DbSet<Product> _products;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _products = dbContext.Products;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _products.AsNoTracking().Include(p => p.Category).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        var product = await _products.FindAsync(id);
        if (product is null)
            return null;
        await _products.Entry(product).Reference(p => p.Category).LoadAsync();
        return product;
    }

    public async Task<IEnumerable<Product>> GetByNameAsync(string name)
    {
        return await _products.AsNoTracking()
            .Include(p => p.Category)
            .Where(p => p.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByCreatedAtAsync(DateOnly date)
    {
        return await _products.AsNoTracking()
            .Include(p => p.Category)
            .Where(c => DateOnly.FromDateTime(c.CreatedAt.Date) == date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetByUpdatedAtAsync(DateOnly date)
    {
        return await _products.AsNoTracking()
            .Include(p => p.Category)
            .Where(c => DateOnly.FromDateTime(c.UpdatedAt.Date) == date)
            .ToListAsync();
    }

    public void Add(Product product)
    {
        _products.Add(product);
    }

    public async void Update(Guid id, Product productRequest)
    {
        var product = await _products.FindAsync(id);

        if (product is null)
            return;

        product.UpdateImageUrl(productRequest.ImageUrl);
        product.UpdateName(productRequest.Name);
        product.UpdateDescription(productRequest.Description);
        product.UpdatePrice(productRequest.Price);
        product.UpdateDiscount(productRequest.Discount);
        product.UpdateUpdatedAt();

        _products.Update(product);
    }

    public void Delete(Product product)
    {
        _products.Remove(product);
    }
}

