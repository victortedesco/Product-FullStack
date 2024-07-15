using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;
using Products.Domain.Interfaces.Repositories;
using Products.Infrastructure.Context;

namespace Products.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DbSet<Category> _categories;

    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _categories = dbContext.Categories;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _categories.AsNoTracking().ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _categories.FindAsync(id);
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        return await _categories.AsNoTracking().FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
    }

    public async Task<IEnumerable<Category>> GetByCreatedAtAsync(DateOnly date)
    {
        return await _categories.AsNoTracking()
            .Where(c => DateOnly.FromDateTime(c.CreatedAt.Date) == date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetByUpdatedAtAsync(DateOnly date)
    {
        return await _categories.AsNoTracking()
            .Where(c => DateOnly.FromDateTime(c.UpdatedAt.Date) == date)
            .ToListAsync();
    }

    public void Add(Category category)
    {
        _categories.Add(category);
    }

    public async void Update(Guid id, Category category)
    {
        var existingCategory = await GetByIdAsync(id);
        if (existingCategory is null)
            return;
        existingCategory.UpdateName(category.Name);
        existingCategory.UpdateDescription(category.Description);
        existingCategory.UpdateUpdatedAt();
        _categories.Update(category);
    }

    public void Delete(Category category)
    {
        _categories.Remove(category);
    }
}
