using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;
using Products.Domain.Interfaces;
using Products.Domain.Interfaces.Repository;
using Products.Infrastructure.Data;

namespace Products.Infrastructure.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly DbSet<Category> _categories;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _categories = dbContext.Categories;
        _unitOfWork = new UnitOfWork(dbContext);
    }

    public IUnitOfWork UnitOfWork { get => _unitOfWork; }

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

    public async Task<string> AddAsync(Category category)
    {
        if (!Category.IsValidName(category.Name))
            return "Invalid category name.";
        if (!Category.IsValidDescription(category.Description))
            return "Invalid category description.";

        var existingCategory = await GetByNameAsync(category.Name);

        if (existingCategory is not null)
            return "This category already exists!";

        await _categories.AddAsync(category);

        return "Ok";
    }

    public async Task<string> UpdateAsync(Guid id, Category categoryRequest)
    {
        var category = await _categories.FindAsync(id);

        if (category is null)
            return "Not Found";

        if (!category.UpdateName(categoryRequest.Name))
            return "Invalid category name.";
        if (!category.UpdateDescription(categoryRequest.Description))
            return "Invalid category description.";

        _categories.Update(category);
        return "Ok";
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var product = await _categories.FindAsync(id);

        if (product is null)
            return false;

        _categories.Remove(product);
        return true;
    }
}
