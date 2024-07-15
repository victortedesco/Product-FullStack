using FluentResults;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;
using Products.Domain.Interfaces.Services;

namespace Products.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Product>> GetByCreatedAtAsync(DateOnly date)
    {
        return await _productRepository.GetByCreatedAtAsync(date);
    }

    public async Task<IEnumerable<Product>> GetByUpdatedAtAsync(DateOnly date)
    {
        return await _productRepository.GetByUpdatedAtAsync(date);
    }

    public async Task<IEnumerable<Product>> GetByNameAsync(string name)
    {
        return await _productRepository.GetByNameAsync(name);
    }

    public Task<Result> AddAsync(Product product)
    {
        var errors = new List<Error>();

        if (!Product.IsValidName(product.Name))
            errors.Add(ProductErrors.InvalidName);
        if (!Product.IsValidDescription(product.Description))
            errors.Add(ProductErrors.InvalidDescription);
        if (!Product.IsValidPrice(product.Price))
            errors.Add(ProductErrors.InvalidPrice);
        if (!Product.IsValidDiscount(product.Discount))
            errors.Add(ProductErrors.InvalidDiscount);

        if (errors.Count != 0)
            return Task.FromResult(Result.Fail(errors));

        _productRepository.Add(product);

        return Task.FromResult(Result.Ok());
    }


    public async Task<Result> UpdateAsync(Guid id, Product product)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);

        if (existingProduct is null)
            return Result.Fail(ProductErrors.DoesNotExist);

        var errors = new List<Error>();

        if (!Product.IsValidName(product.Name))
            errors.Add(ProductErrors.InvalidName);
        if (!Product.IsValidDescription(product.Description))
            errors.Add(ProductErrors.InvalidDescription);
        if (!Product.IsValidPrice(product.Price))
            errors.Add(ProductErrors.InvalidPrice);
        if (!Product.IsValidDiscount(product.Discount))
            errors.Add(ProductErrors.InvalidDiscount);

        if (errors.Count != 0)
            return Result.Fail(errors);

        _productRepository.Update(id, product);

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null)
            return Result.Fail(ProductErrors.DoesNotExist);

        _productRepository.Delete(product);

        return Result.Ok();
    }
}
