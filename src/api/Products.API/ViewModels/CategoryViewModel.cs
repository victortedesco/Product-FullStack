using Products.Domain.Entities;

namespace Products.API.ViewModels;

public class CategoryViewModel
{
    private CategoryViewModel(Guid id, string name, string description, DateTime createdAt, DateTime lastModifiedAt)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        LastModifiedAt = lastModifiedAt;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime LastModifiedAt { get; init; }

    public static CategoryViewModel FromModel(Category category)
    {
        return new CategoryViewModel(category.Id, category.Name, category.Description, category.CreatedAt, category.UpdatedAt);
    }
}
