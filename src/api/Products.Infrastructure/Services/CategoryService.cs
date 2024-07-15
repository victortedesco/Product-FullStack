using FluentResults;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;
using Products.Domain.Interfaces.Services;

namespace Products.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await _categoryRepository.GetByNameAsync(name);
    }

    public async Task<IEnumerable<Category>> GetByCreatedAtAsync(DateOnly date)
    {
        return await _categoryRepository.GetByCreatedAtAsync(date);
    }

    public async Task<IEnumerable<Category>> GetByUpdatedAtAsync(DateOnly date)
    {
        return await _categoryRepository.GetByUpdatedAtAsync(date);
    }

    public async Task<Result> AddAsync(Category category)
    {
        var existingCategory = await GetByNameAsync(category.Name);

        if (existingCategory is not null)
            return Result.Fail(CategoryErrors.AlreadyExists);

        var errors = new List<Error>();

        if (!Category.IsValidName(category.Name))
            errors.Add(CategoryErrors.InvalidName);
        if (!Category.IsValidDescription(category.Description))
            errors.Add(CategoryErrors.InvalidDescription);

        if (errors.Count != 0)
            return Result.Fail(errors);

        _categoryRepository.Add(category);

        return Result.Ok();
    }

    public async Task<Result> UpdateAsync(Guid id, Category category)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(id);

        if (existingCategory is null)
            return Result.Fail(CategoryErrors.DoesNotExist);

        var errors = new List<Error>();

        if (!Category.IsValidName(category.Name))
            errors.Add(CategoryErrors.InvalidName);
        if (!Category.IsValidDescription(category.Description))
            errors.Add(CategoryErrors.InvalidDescription);

        if (errors.Count != 0)
            return Result.Fail(errors);

        _categoryRepository.Update(id, category);

        return Result.Ok();
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);

        if (category is null)
            return Result.Fail(CategoryErrors.DoesNotExist);

        _categoryRepository.Delete(category);

        return Result.Ok();
    }
}
